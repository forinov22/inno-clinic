using Innowise.Common.Exceptions;

namespace Innowise.Offices.Application.Offices.Exceptions;

public class InvalidOfficeStatusException : BadRequestException
{
    private const string ExceptionMessage = "Invalid office status";

    public InvalidOfficeStatusException() : base(ExceptionMessage)
    {
    }

    public InvalidOfficeStatusException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}