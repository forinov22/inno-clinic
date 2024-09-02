using Auth.Application.Accounts.Common;
using MediatR;

namespace Auth.Application.Accounts.Commands.SignUpStuff;

public record SignUpStuffAccountCommand(string Email, string Role) : IRequest<AccountResult>;