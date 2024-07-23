using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Innowise.Common.Exceptions;

public class ValidationExceptionHandlingMiddleware(ILogger<ValidationExceptionHandlingMiddleware> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        logger.LogError("Error occurred: {Message}", validationException.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "ValidationFailure",
            Title = "Validation error",
            Detail = "One or more validation errors has occurred"
        };

        if (validationException.Errors is not null)
        {
            problemDetails.Extensions["errors"] = validationException.Errors.Select(error => new { error.PropertyName, error.ErrorMessage });
        }

        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);

        return true;
    }
}