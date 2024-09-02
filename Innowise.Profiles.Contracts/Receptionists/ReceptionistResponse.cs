namespace Innowise.Profiles.Contracts.Receptionists;

public record ReceptionistResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string WorkerStatus,
    Guid AccountId);