using Innowise.Common.Exceptions;

namespace Appointments.Application.Appointments.Exceptions;

public class ResultNotFoundException : NotFoundException
{
    private const string ExceptionMessage = "Result not found";

    public ResultNotFoundException() : base(ExceptionMessage)
    {
    }

    public ResultNotFoundException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}