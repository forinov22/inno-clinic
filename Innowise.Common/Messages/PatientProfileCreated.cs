using System.Text.Json.Serialization;

namespace Innowise.Common.Messages;

public class PatientProfileCreated
{
    [JsonPropertyName("ProfileId")]
    public Guid ProfileId { get; set; }
    [JsonPropertyName("FirstName")]
    public string FirstName { get; set; } = string.Empty;
    [JsonPropertyName("LastName")]
    public string LastName { get; set; } = string.Empty;
    [JsonPropertyName("MiddleName")]
    public string? MiddleName { get; set; }
    [JsonPropertyName("Email")]
    public string? Email { get; set; }
}