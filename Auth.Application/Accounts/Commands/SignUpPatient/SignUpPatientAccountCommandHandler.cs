using System.Security.Cryptography;
using System.Text;
using Auth.Application.Accounts.Common;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Domain.Exceptions;
using InnoClinic.Contracts;
using InnoClinic.Services.Email;
using MassTransit;
using MediatR;

namespace Auth.Application.Accounts.Commands.SignUpPatient;

public class SignUpPatientAccountCommandHandler(
    IUnitOfWork unitOfWork,
    ITokenGenerator tokenGenerator,
    IEmailService emailService,
    IPublishEndpoint publishEndpoint) : IRequestHandler<SignUpPatientAccountCommand, AuthResult>
{
    public async Task<AuthResult> Handle(SignUpPatientAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await unitOfWork.AccountRepository.IsEmailUniqueAsync(request.Email))
        {
            throw new BadRequestException("Email is already in use");
        }

        var account = new Account()
        {
            Email = request.Email,
            IsEmailVerified = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ActivationLink = Guid.NewGuid().ToString(),
            Role = Role.Patient
        };

        using (var hmac = new HMACSHA256())
        {
            account.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            account.PasswordSalt = hmac.Key;
        }

        unitOfWork.AccountRepository.Create(account);
        await unitOfWork.SaveAllAsync();

        await publishEndpoint.Publish(new PatientAccountCreated()
        {
            AccountId = account.AccountId,
            Email = account.Email,
            PhoneNumber = account.PhoneNumber,
            ActivationLink = account.ActivationLink,
            IsEmailVerified = account.IsEmailVerified,
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt,
            PhotoUrl = account.PhotoUrl,
            Role = account.Role.ToString()
        }, cancellationToken);

        var verificationUrl = $"https://localhost:7139/verify-email?activationLink={account.ActivationLink}";
        var emailBody = $"Please verify your email by clicking <a href=\"{verificationUrl}\">here</a>";
        await emailService.SendEmailAsync(account.Email, "Verify your email", emailBody);

        var accessToken = tokenGenerator.GenerateToken(account, 3);
        var refreshToken = tokenGenerator.GenerateToken(account, 30);

        var authResult = new AuthResult(AccountResult.MapFromAccount(account), accessToken, refreshToken);
        return authResult; 
    }
}