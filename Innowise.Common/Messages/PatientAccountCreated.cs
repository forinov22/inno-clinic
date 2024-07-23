namespace Innowise.Common.Messages;

public class PatientAccountCreated
{
    public Guid AccountId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string ActivationLink { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? PhotoUrl { get; set; }
    public string Role { get; set; } = string.Empty;
}