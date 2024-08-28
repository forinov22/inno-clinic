using Innowise.Common.Exceptions;

namespace Innowise.Services.Application.ServiceCategories.Exceptions;

public class ServiceCategoryNotFoundException : NotFoundException
{
    private const string ExceptionMessage = "Service category not found";

    public ServiceCategoryNotFoundException() : base(ExceptionMessage)
    {
    }

    public ServiceCategoryNotFoundException(Exception inner) : base(ExceptionMessage, inner)
    {
    }
}