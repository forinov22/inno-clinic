namespace Innowise.Profiles.Contracts.Patients;

public record ProfileMatchesRequest(
    string FirstName,
    string LastName,
    string MiddleName,
    DateOnly DateOfBirth);