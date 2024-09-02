using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;

namespace Profiles.Infrastructure.Persistence.Repositories;

public class OfficeRepository(
    HttpClient httpClient,
    IDistributedCache distributedCache,
    IConfiguration configuration,
    ILogger<OfficeRepository> logger)
    : IOfficeRepository
{
    public async Task<Office?> GetOfficeByIdAsync(Guid officeId)
    {
        var officeKey = $"office-{officeId}";
        var officeString = await distributedCache.GetStringAsync(officeKey);

        if (officeString is null)
        {
            await FetchOfficesAsync();
        }

        officeString = await distributedCache.GetStringAsync(officeKey);

        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
        options.Converters.Add(new OfficeStatusJsonConverter());
        
        return officeString is null ? null : JsonSerializer.Deserialize<Office>(officeString);
    }

    public async Task FetchOfficesAsync()
    {
        try
        {
            var officeApiUrl = configuration["Urls:Offices"]
                            ?? throw new Exception("Office api url is not configured");

            var response = await httpClient.GetAsync($"{officeApiUrl}/api/offices");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch offices");
            }

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            options.Converters.Add(new OfficeStatusJsonConverter());
            
            var offices = await response.Content.ReadFromJsonAsync<IEnumerable<Office>>(options)
                       ?? throw new Exception("Failed to deserialize offices");

            foreach (var office in offices)
            {
                var key = $"office-{office.Id}";
                await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(office));
            }
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "Failed to fetch offices");
            throw;
        }
    }
}

// to read officeStatus properly
public class OfficeStatusJsonConverter : JsonConverter<OfficeStatus>
{
    public override OfficeStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        if (!Enum.TryParse(str, out OfficeStatus status))
        {
            throw new JsonException($"Unable to parse '{str}' as an OfficeStatus");
        }
        return status;
    }

    public override void Write(Utf8JsonWriter writer, OfficeStatus value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}