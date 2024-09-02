using MediatR;

namespace Profiles.Application.Patients.Commands.LinkToAccount;

public record LinkPatientProfileToAccountCommand(Guid ProfileId, Guid AccountId) : IRequest;