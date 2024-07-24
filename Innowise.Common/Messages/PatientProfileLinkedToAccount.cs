using System.Text.Json.Serialization;

namespace Innowise.Common.Messages;

public class PatientProfileLinkedToAccount
{
    [JsonPropertyName("ProfileId")]
    public Guid ProfileId { get; set; }
    [JsonPropertyName("Email")]
    public string Email { get; set; } = string.Empty;
}