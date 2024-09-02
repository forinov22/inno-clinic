using Innowise.Common.Exceptions;

namespace Profiles.Application.Doctors.Exceptions;

public class SpecializationNotFoundException : NotFoundException
{
    private const string ExceptionMessage = "Specialization not found";

    public SpecializationNotFoundException() : base(ExceptionMessage)
    {
    }

    public SpecializationNotFoundException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}