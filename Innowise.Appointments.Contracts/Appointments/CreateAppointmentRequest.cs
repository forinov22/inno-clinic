namespace Innowise.Appointments.Contracts.Appointments;

public record CreateAppointmentRequest(
    Guid PatientId,
    Guid DoctorId,
    Guid ServiceId,
    DateTime StartDate);