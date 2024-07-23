using System.Net.Http.Json;
using System.Text.Json;
using Appointments.Application.Interfaces.HttpClients;
using Appointments.Application.Interfaces.HttpClients.Services;
using Appointments.Application.Interfaces.Repositories;
using Appointments.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Appointments.Infrastructure.Persistence.Repositories;

// public class ServiceHttpClient(
//     HttpClient httpClient,
//     IDistributedCache distributedCache,
//     IConfiguration configuration,
//     ILogger<ServiceHttpClient> logger) : IServiceHttpClient
// {
//     public async Task<Service?> GetServiceByIdAsync(Guid serviceId)
//     {
//         var serviceKey = $"service-{serviceId}";
//         var serviceString = await distributedCache.GetStringAsync(serviceKey);
//
//         if (serviceString is null)
//         {
//             await FetchServicesAsync();
//         }
//
//         serviceString = await distributedCache.GetStringAsync(serviceKey);
//
//         return serviceString is null ? null : JsonSerializer.Deserialize<Service>(serviceString);
//     }
//
//     public async Task FetchServicesAsync()
//     {
//         try
//         {
//             var serviceApiUrl = configuration["Urls:Services"]
//                              ?? throw new Exception("Service api url is not configured");
//
//             var response = await httpClient.GetAsync($"{serviceApiUrl}/api/services");
//             if (!response.IsSuccessStatusCode)
//             {
//                 throw new Exception("Failed to fetch services");
//             }
//
//             var servicesStr = await response.Content.ReadAsStringAsync();
//             var services = await response.Content.ReadFromJsonAsync<IEnumerable<Service>>()
//                                ?? throw new Exception("Failed to deserialize services");
//
//             foreach (var service in services)
//             {
//                 var key = $"service-{service.Id}";
//                 await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(service));
//             }
//         }
//         catch (HttpRequestException e)
//         {
//             logger.LogError(e, "Failed to fetch services");
//             throw;
//         }
//     }
// }