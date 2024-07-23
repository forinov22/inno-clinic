namespace Innowise.Appointments.Contracts.Appointments;

public record ResultResponse(
    Guid Id,
    string Complaints,
    string Recommendations,
    Guid AppointmentId,
    DateTime DateTime,
    Guid PatientId,
    string PatientFirstName,
    string PatientLastName,
    string? PatientMiddleName,
    Guid DoctorId,
    string DoctorFirstName,
    string DoctorLastName,
    string? DoctorMiddleName,
    Guid ServiceId,
    string ServiceName);