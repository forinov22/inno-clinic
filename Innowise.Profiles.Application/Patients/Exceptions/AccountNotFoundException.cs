using Innowise.Common.Exceptions;

namespace Profiles.Application.Patients.Exceptions;

public class AccountNotFoundException : NotFoundException
{
    private const string ExceptionMessage = "Account not found";

    public AccountNotFoundException() : base(ExceptionMessage)
    {
    }

    public AccountNotFoundException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}