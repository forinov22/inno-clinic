using Auth.Application.Accounts.Common;
using MediatR;

namespace Auth.Application.Accounts.Commands.Refresh;

public record RefreshAccountCommand(string RefreshToken) : IRequest<AuthResult>;