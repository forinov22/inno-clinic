namespace Innowise.Profiles.Contracts.Receptionists;

public record CreateReceptionistRequest(
    string Email,
    string FirstName,
    string LastName,
    string? MiddleName,
    string WorkerStatus);