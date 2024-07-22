using Innowise.Common.Exceptions;

namespace Appointments.Application.Services.Exceptions;

public class ServiceNotFoundException : NotFoundException
{
    private const string ExceptionMessage = "Service not found";

    public ServiceNotFoundException() : base(ExceptionMessage)
    {
    }

    public ServiceNotFoundException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}