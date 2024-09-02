using Profiles.Domain.Entities;

namespace Profiles.Application.Interfaces.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(Guid patientId);
    Task<IEnumerable<Patient>> FindMatchesAsync(string firstName, string lastName, string? middleName, DateOnly dateOfBirth);
    void Add(Patient patient);
}