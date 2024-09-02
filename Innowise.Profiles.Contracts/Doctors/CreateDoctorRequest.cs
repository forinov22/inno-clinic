using Microsoft.AspNetCore.Http;

namespace Innowise.Profiles.Contracts.Doctors;

public record CreateDoctorRequest(
    string Email,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    IFormFile Photo,
    Guid SpecializationId,
    Guid OfficeId,
    DateOnly CareerStartYear,
    string WorkerStatus);