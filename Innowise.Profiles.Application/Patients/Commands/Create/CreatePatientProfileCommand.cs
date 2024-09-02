using MediatR;
using Profiles.Application.Patients.Common;

namespace Profiles.Application.Patients.Commands.Create;

public record CreatePatientProfileCommand(
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    Guid? AccountId) : IRequest<PatientResult>;