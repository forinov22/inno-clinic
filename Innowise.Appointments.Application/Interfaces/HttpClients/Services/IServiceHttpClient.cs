namespace Appointments.Application.Interfaces.HttpClients.Services;

public interface IServiceHttpClient
{
    Task<ServiceResponse> GetServiceByIdAsync(ServiceRequest serviceRequest);
}