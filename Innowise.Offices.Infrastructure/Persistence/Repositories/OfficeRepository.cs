using Innowise.Offices.Application.Interfaces.Repositories;
using Innowise.Offices.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Innowise.Offices.Infrastructure.Persistence.Repositories;

public class OfficeRepository(OfficesDbContext context) : IOfficeRepository
{
    public async Task<IEnumerable<Office>> GetAllAsync()
    {
        return await context.Offices.AsNoTracking().ToListAsync();
    }

    public async Task<Office?> GetByIdAsync(Guid officeId)
    {
        return await context.Offices.FirstOrDefaultAsync(office => office.Id == officeId);
    }

    public void Add(Office office)
    {
        context.Offices.Add(office);
    }

    public void Update(Office office)
    {
        context.Offices.Update(office);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}