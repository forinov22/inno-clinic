using System.Security.Cryptography;
using System.Text;
using Auth.Application.Accounts.Common;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Domain.Exceptions;
using InnoClinic.Services.Email;
using MediatR;

namespace Auth.Application.Accounts.Commands.SignUpStuff;

internal class SignUpStuffAccountCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordGenerator passwordGenerator,
    IEmailService emailService) : IRequestHandler<SignUpStuffAccountCommand, AccountResult>
{
    public async Task<AccountResult> Handle(SignUpStuffAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await unitOfWork.AccountRepository.IsEmailUniqueAsync(request.Email))
        {
            throw new BadRequestException("Email is already in use");
        }

        var account = new Account()
        {
            Email = request.Email,
            ActivationLink = Guid.NewGuid().ToString(),
            IsEmailVerified = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Role = Enum.Parse<Role>(request.Role)
        };

        using var hmac = new HMACSHA256();
        
        var passwordStr = passwordGenerator.GeneratePassword();
        account.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordStr));
        account.PasswordSalt = hmac.Key;
        
        unitOfWork.AccountRepository.Create(account);
        await unitOfWork.SaveAllAsync();

        var emailBody = $"<p>Email: {account.Email}</p>" +
                        $"<p>Password: {passwordStr}</p>";

        await emailService.SendEmailAsync(account.Email, "Account Credentials", emailBody);

        return AccountResult.MapFromAccount(account);
    }
}