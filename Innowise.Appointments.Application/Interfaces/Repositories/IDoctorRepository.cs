using Appointments.Domain.Entities;

namespace Appointments.Application.Interfaces.Repositories;

public interface IDoctorRepository
{
    Task<Doctor?> GetByIdAsync(Guid doctorId);
    void Add(Doctor doctor);
}