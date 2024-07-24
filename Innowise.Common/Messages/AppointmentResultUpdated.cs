using System.Text.Json.Serialization;
using Appointments.Domain.Entities;

namespace Innowise.Common.Messages;

public class AppointmentResultUpdated
{
    [JsonPropertyName("Email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("Result")]
    public Result Result { get; set; } = null!;
}