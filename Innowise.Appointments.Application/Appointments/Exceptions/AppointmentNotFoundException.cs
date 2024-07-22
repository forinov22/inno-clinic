using Innowise.Common.Exceptions;

namespace Appointments.Application.Appointments.Exceptions;

public sealed class AppointmentNotFoundException : NotFoundException
{
    private const string ExceptionMessage = "Appointment not found";

    public AppointmentNotFoundException() : base(ExceptionMessage)
    {
    }

    public AppointmentNotFoundException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}