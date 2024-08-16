using Innowise.Common.Exceptions;

namespace Appointments.Application.Appointments.Exceptions;

public class TimeSlotIsAlreadyInUseException : BadRequestException
{
    private const string ExceptionMessage = "This time slot is already booked for this doctor";

    public TimeSlotIsAlreadyInUseException() : base(ExceptionMessage)
    {
    }

    public TimeSlotIsAlreadyInUseException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}