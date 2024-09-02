using Profiles.Domain.Entities;

namespace Profiles.Application.Interfaces.Repositories;

public interface IDoctorRepository
{
    Task<Doctor?> GetByIdAsync(Guid doctorId);
    Task<IEnumerable<Doctor>> GetAllAtWorkAsync();
    void Add(Doctor doctor);
}