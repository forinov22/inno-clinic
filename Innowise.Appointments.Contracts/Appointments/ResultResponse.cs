using System.Text.Json.Serialization;

namespace Innowise.Appointments.Contracts.Appointments;

public record ResultResponse(
    [property:JsonPropertyName("Id")]
    Guid Id,
    [property:JsonPropertyName("Complaints")]
    string Complaints,
    [property:JsonPropertyName("Recommendations")]
    string Recommendations,
    [property:JsonPropertyName("AppointmentId")]
    Guid AppointmentId,
    [property:JsonPropertyName("DateTime")]
    DateTime DateTime,
    [property:JsonPropertyName("PatientId")]
    Guid PatientId,
    [property:JsonPropertyName("PatientFirstName")]
    string PatientFirstName,
    [property:JsonPropertyName("PatientLastName")]
    string PatientLastName,
    [property:JsonPropertyName("PatientMiddleName")]
    string? PatientMiddleName,
    [property:JsonPropertyName("DoctorId")]
    Guid DoctorId,
    [property:JsonPropertyName("DoctorFirstName")]
    string DoctorFirstName,
    [property:JsonPropertyName("DoctorLastName")]
    string DoctorLastName,
    [property:JsonPropertyName("DoctorMiddleName")]
    string? DoctorMiddleName,
    [property:JsonPropertyName("ServiceId")]
    Guid ServiceId,
    [property:JsonPropertyName("ServiceName")]
    string ServiceName);