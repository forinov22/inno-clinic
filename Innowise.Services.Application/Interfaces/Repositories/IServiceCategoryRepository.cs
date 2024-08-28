using Services.Domain;

namespace Innowise.Services.Application.Interfaces.Repositories;

public interface IServiceCategoryRepository
{
    Task<IEnumerable<ServiceCategory>> GetAllAsync();
    Task<ServiceCategory?> GetByIdAsync(Guid serviceCategoryId);
    void Add(ServiceCategory serviceCategory);
    void Update(ServiceCategory serviceCategory);
    Task SaveAllAsync();
}