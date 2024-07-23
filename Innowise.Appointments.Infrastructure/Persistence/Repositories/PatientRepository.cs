using Appointments.Application.Interfaces.Repositories;
using Appointments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Infrastructure.Persistence.Repositories;

internal class PatientRepository(AppointmentsDbContext context) : IPatientRepository
{
    public Task<Patient?> GetByIdAsync(Guid patientId)
    {
        return context.Patients.FirstOrDefaultAsync(patient => patient.ExternalId == patientId);
    }

    public void Add(Patient patient)
    {
        context.Patients.Add(patient);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}