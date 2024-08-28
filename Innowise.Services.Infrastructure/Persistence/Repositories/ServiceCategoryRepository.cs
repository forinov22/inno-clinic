using Innowise.Services.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Domain;

namespace Innowise.Services.Infrastructure.Persistence.Repositories;

public class ServiceCategoryRepository(ServicesDbContext context) : IServiceCategoryRepository
{
    public async Task<IEnumerable<ServiceCategory>> GetAllAsync()
    {
        return await context.ServiceCategories.ToListAsync();
    }

    public async Task<ServiceCategory?> GetByIdAsync(Guid serviceCategoryId)
    {
        return await context.ServiceCategories.FirstOrDefaultAsync(serviceCategory => serviceCategory.Id == serviceCategoryId);
    }

    public void Add(ServiceCategory serviceCategory)
    {
        context.ServiceCategories.Add(serviceCategory);
    }

    public void Update(ServiceCategory serviceCategory)
    {
        context.ServiceCategories.Update(serviceCategory);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}