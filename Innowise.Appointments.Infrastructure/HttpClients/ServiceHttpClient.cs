using System.Net.Http.Json;
using Appointments.Application.Interfaces.HttpClients.Services;
using Microsoft.Extensions.Logging;

namespace Appointments.Infrastructure.HttpClients;

public class ServiceHttpClient(HttpClient httpClient, ILogger<ServiceHttpClient> logger) : IServiceHttpClient
{
    public async Task<ServiceResponse> GetServiceByIdAsync(ServiceRequest serviceRequest)
    {
        try
        {
            var response = await httpClient.GetAsync($"api/services/{serviceRequest.ServiceId}");
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<ServiceResponse>())!;
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "An error occurred while fetching service.");
            throw;
        }
    }
}