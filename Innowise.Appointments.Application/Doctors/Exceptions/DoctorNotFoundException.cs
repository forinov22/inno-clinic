using Innowise.Common.Exceptions;

namespace Appointments.Application.Doctors.Exceptions;

public class DoctorNotFoundException : NotFoundException
{
    private const string ExceptionMessage = "Doctor not found";

    public DoctorNotFoundException() : base(ExceptionMessage)
    {
    }

    public DoctorNotFoundException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}