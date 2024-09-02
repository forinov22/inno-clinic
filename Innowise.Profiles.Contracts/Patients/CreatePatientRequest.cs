namespace Innowise.Profiles.Contracts.Patients;

public record CreatePatientRequest(
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    Guid? AccountId);