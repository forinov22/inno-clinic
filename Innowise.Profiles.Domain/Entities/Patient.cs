namespace Profiles.Domain.Entities;

public class Patient
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public bool IsLinkedToAccount => AccountId is not null;
    public Guid? AccountId { get; set; }
    public Account? Account { get; set; }
}