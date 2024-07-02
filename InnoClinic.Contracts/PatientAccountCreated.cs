namespace InnoClinic.Contracts;

public class PatientAccountCreated
{
    public Guid AccountId { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string ActivationLink { get; set; }
    public bool IsEmailVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? PhotoUrl { get; set; }
    public string Role { get; set; }
}