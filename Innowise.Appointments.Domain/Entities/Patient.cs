namespace Appointments.Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public Guid ExternalId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
}