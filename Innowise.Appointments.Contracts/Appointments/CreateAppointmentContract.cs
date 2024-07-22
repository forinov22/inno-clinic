namespace Innowise.Appointments.Contracts.Appointments;

/*
 * Guid PatientId,
    Guid DoctorId,
    Guid ServiceId,
    DateTime StartDate
 */

public record CreateAppointmentContract(
    Guid PatientId,
    Guid DoctorId,
    Guid ServiceId,
    DateTime StartDate);