using System.Security.Cryptography;
using System.Text;
using Auth.Application.Accounts.Common;
using Auth.Application.Interfaces;
using Auth.Domain.Exceptions;
using MediatR;

namespace Auth.Application.Accounts.Commands.SignIn;

public class SignInAccountCommandHandler(IUnitOfWork unitOfWork, ITokenGenerator tokenGenerator)
    : IRequestHandler<SignInAccountCommand, AuthResult>
{
    private const int AccessLifeTime = 3;
    private const int RefreshLifeTime = 30;
    
    public async Task<AuthResult> Handle(SignInAccountCommand request, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.AccountRepository.GetByEmailAsync(request.Email);

        if (candidate is null)
        {
            throw new UnauthorizedException("Invalid credentials");
        }

        using (var hmac = new HMACSHA256(candidate.PasswordSalt))
        {
            if (!candidate.PasswordHash.SequenceEqual(hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password))))
            {
                throw new UnauthorizedException("Invalid credentials");
            }
        }

        var accessToken = tokenGenerator.GenerateToken(candidate, AccessLifeTime);
        var refreshToken = tokenGenerator.GenerateToken(candidate, RefreshLifeTime);

        var authResult = new AuthResult(AccountResult.MapFromAccount(candidate), accessToken, refreshToken);
        return authResult;
    }
}