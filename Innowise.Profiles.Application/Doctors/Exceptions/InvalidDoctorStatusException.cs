using Innowise.Common.Exceptions;

namespace Profiles.Application.Doctors.Exceptions;

public class InvalidDoctorStatusException : BadRequestException
{
    private const string ExceptionMessage = "Invalid doctor status";

    public InvalidDoctorStatusException() : base(ExceptionMessage)
    {
    }

    public InvalidDoctorStatusException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}