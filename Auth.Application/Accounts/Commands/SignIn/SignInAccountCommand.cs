using Auth.Application.Accounts.Common;
using MediatR;

namespace Auth.Application.Accounts.Commands.SignIn;

public record SignInAccountCommand(string Email, string Password) : IRequest<AuthResult>;