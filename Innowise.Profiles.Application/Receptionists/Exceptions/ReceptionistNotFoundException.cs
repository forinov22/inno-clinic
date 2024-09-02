using Innowise.Common.Exceptions;

namespace Profiles.Application.Receptionists.Exceptions;

public class ReceptionistNotFoundException : NotFoundException
{
    private const string ExceptionMessage = "Receptionist not found";

    public ReceptionistNotFoundException() : base(ExceptionMessage)
    {
    }

    public ReceptionistNotFoundException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}