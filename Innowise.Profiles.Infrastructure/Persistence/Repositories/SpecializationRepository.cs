using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;

namespace Profiles.Infrastructure.Persistence.Repositories;

public class SpecializationRepository(
    HttpClient httpClient,
    IDistributedCache distributedCache,
    IConfiguration configuration,
    ILogger<SpecializationRepository> logger) : ISpecializationRepository
{
    public async Task<Specialization?> GetSpecializationByIdAsync(Guid specializationId)
    {
        var specializationKey = $"specialization-{specializationId}";
        var specializationString = await distributedCache.GetStringAsync(specializationKey);

        if (specializationString is null)
        {
            await FetchSpecializationsAsync();
        }

        specializationString = await distributedCache.GetStringAsync(specializationKey);

        return specializationString is null ? null : JsonSerializer.Deserialize<Specialization>(specializationString);
    }

    public async Task FetchSpecializationsAsync()
    {
        try
        {
            var serviceApiUrl = configuration["Urls:Services"]
                            ?? throw new Exception("Service api url is not configured");

            var response = await httpClient.GetAsync($"{serviceApiUrl}/api/specializations");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch specializations");
            }

            var specializations = await response.Content.ReadFromJsonAsync<IEnumerable<Specialization>>()
                       ?? throw new Exception("Failed to deserialize specializations");

            foreach (var specialization in specializations)
            {
                var key = $"specialization-{specialization.Id}";
                await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(specialization));
            }
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "Failed to fetch specializations");
            throw;
        }
    }
}