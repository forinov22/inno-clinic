namespace Profiles.Application.Patients.Common;

public record PatientResult(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    bool IsLinkedToAccount,
    Guid? AccountId);