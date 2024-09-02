using Innowise.Common.Exceptions;

namespace Profiles.Application.Doctors.Exceptions;

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