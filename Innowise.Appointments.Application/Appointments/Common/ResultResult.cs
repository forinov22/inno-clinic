namespace Appointments.Application.Appointments.Common;

public record ResultResult(
    Guid Id,
    string Complaints,
    string Conclusion,
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