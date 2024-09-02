using MediatR;

namespace Auth.Application.Accounts.Commands.VerifyEmail;

public record VerifyEmailCommand(string ActivationLink) : IRequest;