using Appointments.Domain.Entities;

namespace Appointments.Application.Interfaces.Repositories;

public interface IServiceRepository
{
    Task<Service?> GetServiceByIdAsync(Guid serviceId);
    Task FetchServicesAsync();
}