using Services.Domain;

namespace Innowise.Services.Application.Interfaces.Repositories;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(Guid serviceId);
    void Add(Service service);
    void Update(Service service);
    Task SaveAllAsync();
}