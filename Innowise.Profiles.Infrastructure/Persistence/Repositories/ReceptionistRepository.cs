using Microsoft.EntityFrameworkCore;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;

namespace Profiles.Infrastructure.Persistence.Repositories;

internal class ReceptionistRepository(ProfilesDbContext context) : IReceptionistRepository
{
    public async Task<Receptionist?> GetByIdAsync(Guid receptionistId)
    {
        return await context.Receptionists.FirstOrDefaultAsync(receptionist => receptionist.Id == receptionistId);
    }

    public void Add(Receptionist receptionist)
    {
        context.Receptionists.Add(receptionist);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}