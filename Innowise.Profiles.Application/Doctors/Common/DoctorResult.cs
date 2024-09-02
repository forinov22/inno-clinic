using Profiles.Domain.Entities;

namespace Profiles.Application.Doctors.Common;

public record DoctorResult(
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
    Address OfficeAddress,
    Guid SpecializationId,
    string SpecializationName);