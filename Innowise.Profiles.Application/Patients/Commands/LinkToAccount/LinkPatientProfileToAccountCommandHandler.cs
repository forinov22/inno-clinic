using Innowise.Common.Messages;
using MassTransit;
using MediatR;
using Profiles.Application.Interfaces;
using Profiles.Application.Patients.Exceptions;

namespace Profiles.Application.Patients.Commands.LinkToAccount;

internal class LinkPatientProfileToAccountCommandHandler(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
    : IRequestHandler<LinkPatientProfileToAccountCommand>
{
    public async Task Handle(LinkPatientProfileToAccountCommand request, CancellationToken cancellationToken)
    {
        var profile = await unitOfWork.PatientRepository.GetByIdAsync(request.ProfileId);
        if (profile is null)
        {
            throw new PatientNotFoundException();
        }

        var account = await unitOfWork.AccountRepository.GetByExternalIdAsync(request.AccountId);
        if (account is null)
        {
            throw new AccountNotFoundException();
        }

        profile.AccountId = account.AccountId;
        await unitOfWork.SaveAllAsync();

        await publishEndpoint.Publish(new PatientProfileLinkedToAccount()
        {
            ProfileId = profile.Id,
            Email = account.Email,
        }, cancellationToken);
    }
}