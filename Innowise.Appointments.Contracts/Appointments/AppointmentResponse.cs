namespace Innowise.Appointments.Contracts.Appointments;

public record AppointmentResponse(
    Guid Id,
    Guid PatientId,
    string PatientFirstName,
    string PatientLastName,
    string? PatientMiddleName,
    Guid ServiceId,
    string ServiceName,
    string DoctorFirstName,
    string DoctorLastName,
    string? DoctorMiddleName,
    DateTime DateStart,
    DateTime DateEnd);