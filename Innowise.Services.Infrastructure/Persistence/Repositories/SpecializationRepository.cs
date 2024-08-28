using Innowise.Services.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Domain;

namespace Innowise.Services.Infrastructure.Persistence.Repositories;

public class SpecializationRepository(ServicesDbContext context) : ISpecializationRepository
{
    public async Task<IEnumerable<Specialization>> GetAllAsync()
    {
        return await context.Specializations.ToListAsync();
    }

    public async Task<Specialization?> GetByIdAsync(Guid specializationId)
    {
        return await context.Specializations.FirstOrDefaultAsync(specialization => specialization.Id == specializationId);
    }

    public void Add(Specialization specialization)
    {
        context.Specializations.Add(specialization);
    }

    public void Update(Specialization specialization)
    {
        context.Specializations.Update(specialization);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}