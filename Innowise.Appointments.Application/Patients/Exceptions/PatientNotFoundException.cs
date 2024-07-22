using Innowise.Common.Exceptions;

namespace Appointments.Application.Patients.Exceptions;

public class PatientNotFoundException : NotFoundException
{
    private const string ExceptionMessage = "Patient not found";

    public PatientNotFoundException() : base(ExceptionMessage)
    {
    }

    public PatientNotFoundException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}