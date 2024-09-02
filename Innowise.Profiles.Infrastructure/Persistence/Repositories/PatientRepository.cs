using Microsoft.EntityFrameworkCore;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;

namespace Profiles.Infrastructure.Persistence.Repositories;

internal class PatientRepository(ProfilesDbContext context) : IPatientRepository
{
    public async Task<Patient?> GetByIdAsync(Guid patientId)
    {
        return await context.Patients.FirstOrDefaultAsync(patient => patient.Id == patientId);
    }

    public async Task<IEnumerable<Patient>> FindMatchesAsync(string firstName, string lastName, string? middleName, DateOnly dateOfBirth)
    {
        return await context.Patients
            .Where(p =>
                Convert.ToInt32(p.FirstName == firstName) * 5 +
                Convert.ToInt32(p.LastName == lastName) * 5 +
                Convert.ToInt32(p.MiddleName == middleName) * 5 +
                Convert.ToInt32(p.DateOfBirth == dateOfBirth) * 5 >= 13)
            .AsNoTracking()
            .ToListAsync();
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