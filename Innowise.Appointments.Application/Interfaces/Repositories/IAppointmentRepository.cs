using Appointments.Domain.Entities;

namespace Appointments.Application.Interfaces.Repositories;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllUpcomingAsync();
    Task<IEnumerable<Appointment>> GetDoctorUpcomingAsync(Guid doctorId);
    Task<IEnumerable<Appointment>> GetDoctorDateAsync(Guid doctorId, DateTime date);
    Task<IEnumerable<Appointment>> GetPatientHistoryAsync(Guid patientId);
    Task<IEnumerable<Appointment>> GetAllStartingTomorrowAsync();
    Task<Appointment?> GetByIdAsync(Guid appointmentId);
    void Add(Appointment appointment);
    void Remove(Appointment appointment);
}