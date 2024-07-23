using Appointments.Application.Interfaces.Repositories;
using Appointments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Infrastructure.Persistence.Repositories;

internal class DoctorRepository(AppointmentsDbContext context) : IDoctorRepository
{
    public Task<Doctor?> GetByIdAsync(Guid doctorId)
    {
        return context.Doctors.FirstOrDefaultAsync(doctor => doctor.ExternalId == doctorId);
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