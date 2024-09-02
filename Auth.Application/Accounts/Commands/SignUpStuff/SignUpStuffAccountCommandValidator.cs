using Auth.Domain.Entities;
using FluentValidation;

namespace Auth.Application.Accounts.Commands.SignUpStuff;

public class SignUpStuffAccountCommandValidator : AbstractValidator<SignUpStuffAccountCommand>
{
    public SignUpStuffAccountCommandValidator()
    {
        RuleFor(command => command.Email).EmailAddress();
        RuleFor(command => command.Role).IsEnumName(typeof(Role));
    }
}