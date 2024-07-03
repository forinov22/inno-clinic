using MediatR;

namespace Appointments.Application.Doctors.Commands.ProfileCreated;

public record DoctorProfileCreatedCommand(
    Guid ProfileId,
    string FirstName,
    string LastName,
    string? MiddleName) : IRequest;