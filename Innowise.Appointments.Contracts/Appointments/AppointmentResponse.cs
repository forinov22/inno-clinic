using System.Text.Json.Serialization;

namespace Innowise.Appointments.Contracts.Appointments;

public record AppointmentResponse(
    [property:JsonPropertyName("Id")]
    Guid Id,
    [property:JsonPropertyName("PatientId")]
    Guid PatientId,
    [property:JsonPropertyName("PatientFirstName")]
    string PatientFirstName,
    [property:JsonPropertyName("PatientLastName")]
    string PatientLastName,
    [property:JsonPropertyName("PatientMiddleName")]
    string? PatientMiddleName,
    [property:JsonPropertyName("ServiceId")]
    Guid ServiceId,
    [property:JsonPropertyName("ServiceName")]
    string ServiceName,
    [property:JsonPropertyName("DoctorFirstName")]
    string DoctorFirstName,
    [property:JsonPropertyName("DoctorLastName")]
    string DoctorLastName,
    [property:JsonPropertyName("DoctorMiddleName")]
    string? DoctorMiddleName,
    [property:JsonPropertyName("DateStart")]
    DateTime DateStart,
    [property:JsonPropertyName("DateEnd")]
    DateTime DateEnd);