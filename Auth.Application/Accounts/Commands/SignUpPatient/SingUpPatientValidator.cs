using FluentValidation;

namespace Auth.Application.Accounts.Commands.SignUpPatient;

public class SignUpPatientAccountCommandValidator : AbstractValidator<SignUpPatientAccountCommand>
{
    public SignUpPatientAccountCommandValidator()
    {
        RuleFor(patient => patient.Email).EmailAddress();
        RuleFor(patient => patient.Password).MinimumLength(8);
    }
}