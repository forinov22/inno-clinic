namespace Innowise.Profiles.Contracts.Doctors;

public record DoctorResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    string WorkerStatus,
    DateOnly CareerStartYear,
    string PhotoUrl,
    Guid AccountId,
    Guid OfficeId,
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber,
    Guid SpecializationId,
    string SpecializationName);