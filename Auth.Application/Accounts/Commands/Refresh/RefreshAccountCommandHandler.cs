using Auth.Application.Accounts.Common;
using Auth.Application.Interfaces;
using Auth.Domain.Exceptions;
using MediatR;

namespace Auth.Application.Accounts.Commands.Refresh;

internal class RefreshAccountCommandHandler(IUnitOfWork unitOfWork, ITokenGenerator tokenGenerator) : IRequestHandler<RefreshAccountCommand, AuthResult>
{
    public async Task<AuthResult> Handle(RefreshAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await unitOfWork.TokenRepository.GetAccountByRefreshTokenAsync(request.RefreshToken);

        if (account is null)
        {
            throw new NotFoundException("Account not found");
        }

        var accessToken = tokenGenerator.GenerateToken(account, 3);
        var refreshToken = tokenGenerator.GenerateToken(account, 30);

        return new AuthResult(AccountResult.MapFromAccount(account), accessToken, refreshToken);
    }
}