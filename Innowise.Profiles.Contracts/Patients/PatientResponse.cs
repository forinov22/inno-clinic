namespace Innowise.Profiles.Contracts.Patients;

public record PatientResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    bool IsLinkedToAccount,
    Guid? AccountId);