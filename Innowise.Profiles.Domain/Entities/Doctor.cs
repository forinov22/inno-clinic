namespace Profiles.Domain.Entities;

public class Doctor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public WorkerStatus WorkerStatus { get; set; }
    public DateOnly CareerStartYear { get; set; }
    public string PhotoUrl { get; set; }
    public Guid AccountId { get; set; }
    public Guid OfficeId { get; set; }
    public Address OfficeAddress { get; set; }
    public Guid SpecializationId { get; set; }
    public string SpecializationName { get; set; }
}