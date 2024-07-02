using Appointments.Domain.Entities;

namespace Appointments.Application.Interfaces.Repositories;

public interface IResultRepository
{
    Task<Result?> GetByIdAsync(Guid resultId);
    Task<Result?> GetByAppointmentIdAsync(Guid appointmentId);
    void Add(Result result);
    void Update(Result result);
}