namespace Appointments.Domain.Entities;

public class Doctor
{
    public Guid Id { get; set; }
    public Guid ExternalId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
}