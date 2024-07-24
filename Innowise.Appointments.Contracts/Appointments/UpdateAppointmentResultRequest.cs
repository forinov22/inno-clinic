using System.Text.Json.Serialization;

namespace Innowise.Appointments.Contracts.Appointments;

public record UpdateAppointmentResultRequest(
    [property:JsonPropertyName("Complaints")]
    string Complaints,
    [property:JsonPropertyName("Conclusion")]
    string Conclusion,
    [property:JsonPropertyName("Recommendations")]
    string Recommendations);