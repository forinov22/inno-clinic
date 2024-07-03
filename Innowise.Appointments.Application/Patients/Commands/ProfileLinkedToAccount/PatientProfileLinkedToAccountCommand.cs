using MediatR;

namespace Appointments.Application.Patients.Commands.ProfileLinkedToAccount;

public record PatientProfileLinkedToAccountCommand(
    Guid ProfileId,
    string Email) : IRequest;