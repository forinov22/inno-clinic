using Appointments.Domain.Entities;

namespace Appointments.Application.Interfaces.Repositories;

public interface IDoctorRepository
{
    Task<Domain.Entities.Doctor?> GetByIdAsync(Guid doctorId);
    void Add(Domain.Entities.Doctor doctor);
}