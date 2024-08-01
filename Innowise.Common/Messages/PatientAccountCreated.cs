using System.Text.Json.Serialization;

namespace Innowise.Common.Messages;

public class PatientAccountCreated
{
    [JsonPropertyName("AccountId")]
    public Guid AccountId { get; set; }
    [JsonPropertyName("Email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("PhoneNumber")]
    public string? PhoneNumber { get; set; }
    [JsonPropertyName("ActivationLink")]
    public string ActivationLink { get; set; } = string.Empty;
    [JsonPropertyName("IsEmailVerified")]
    public bool IsEmailVerified { get; set; }
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("UpdatedAt")]
    public DateTime UpdatedAt { get; set; }
    [JsonPropertyName("PhotoUrl")]
    public string? PhotoUrl { get; set; }
    [JsonPropertyName("Role")]
    public string Role { get; set; } = string.Empty;
}