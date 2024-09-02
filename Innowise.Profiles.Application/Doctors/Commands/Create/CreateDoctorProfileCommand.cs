using MediatR;
using Microsoft.AspNetCore.Http;
using Profiles.Application.Doctors.Common;

namespace Profiles.Application.Doctors.Commands.Create;

public record CreateDoctorProfileCommand(
    string Email,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    IFormFile Photo,
    Guid SpecializationId,
    Guid OfficeId,
    DateOnly CareerStartYear,
    string WorkerStatus) : IRequest<DoctorResult>;