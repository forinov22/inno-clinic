using MediatR;

namespace Appointments.Application.Patients.Commands.ProfileCreated;

public record PatientProfileCreatedCommand(
    Guid ProfileId,
    string FirstName,
    string LastName,
    string? MiddleName,
    string? Email) : IRequest;