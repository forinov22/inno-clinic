using System.Text.Json.Serialization;

namespace Innowise.Appointments.Contracts.Appointments;

public record CreateAppointmentRequest(
    [property:JsonPropertyName("PatientId")]
    Guid PatientId,
    [property:JsonPropertyName("DoctorId")]
    Guid DoctorId,
    [property:JsonPropertyName("ServiceId")]
    Guid ServiceId,
    [property:JsonPropertyName("StartDate")]
    DateTime StartDate);