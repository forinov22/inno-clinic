using Microsoft.EntityFrameworkCore;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;

namespace Profiles.Infrastructure.Persistence.Repositories;

internal class DoctorRepository(ProfilesDbContext context) : IDoctorRepository
{
    public async Task<Doctor?> GetByIdAsync(Guid doctorId)
    {
        return await context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == doctorId);
    }

    public async Task<IEnumerable<Doctor>> GetAllAtWorkAsync()
    {
        return await context.Doctors.Where(doctor => doctor.WorkerStatus == WorkerStatus.AtWork).ToListAsync();
    }

    public void Add(Doctor doctor)
    {
        context.Doctors.Add(doctor);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}