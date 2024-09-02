namespace Profiles.Application.Receptionists.Common;

public record ReceptionistResult(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string WorkerStatus,
    Guid AccountId);