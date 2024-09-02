using Auth.Application.Accounts.Common;
using MediatR;

namespace Auth.Application.Accounts.Commands.SignUpPatient;

public record SignUpPatientAccountCommand(string Email, string Password) : IRequest<AuthResult>;