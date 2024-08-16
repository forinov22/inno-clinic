using Innowise.Common.Exceptions;

namespace Innowise.Offices.Application.Offices.Exceptions;

public class OfficeNotFoundException : NotFoundException
{
    private const string ExceptionMessage = "Office not found";

    public OfficeNotFoundException() : base(ExceptionMessage)
    {
    }

    public OfficeNotFoundException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}