using Innowise.Services.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Domain;

namespace Innowise.Services.Infrastructure.Persistence.Repositories;

public class ServiceRepository(ServicesDbContext context) : IServiceRepository
{
    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await context.Services.Include(service => service.Specialization)
                            .Include(service => service.ServiceCategory).ToListAsync();
    }

    public async Task<Service?> GetByIdAsync(Guid serviceId)
    {
        return await context.Services.Include(service => service.Specialization)
                            .Include(service => service.ServiceCategory)
                            .FirstOrDefaultAsync(service => service.Id == serviceId);
    }

    public void Add(Service service)
    {
        context.Services.Add(service);
    }

    public void Update(Service service)
    {
        context.Services.Update(service);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}