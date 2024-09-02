using Auth.Application.Interfaces;
using Auth.Domain.Exceptions;
using MediatR;

namespace Auth.Application.Accounts.Commands.VerifyEmail;

internal class VerifyEmailCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<VerifyEmailCommand>
{
    public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var account = await unitOfWork.AccountRepository.GetByActivationLinkAsync(request.ActivationLink);
        if (account is null)
        {
            throw new NotFoundException("Account not found");
        }

        account.IsEmailVerified = true;
        await unitOfWork.SaveAllAsync();
    }
}