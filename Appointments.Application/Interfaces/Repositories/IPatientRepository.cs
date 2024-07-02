using Appointments.Domain.Entities;

namespace Appointments.Application.Interfaces.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(Guid patientId);
    void Add(Patient patient);
}