using System.Net;
using System.Text.Json;
using Auth.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InnoClinic.Exceptions;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
                                                CancellationToken cancellationToken)
    {
        logger.LogError("Error occured: {Message}", exception.Message);

        var statusCode = exception switch
        {
            BadRequestException => (int)HttpStatusCode.BadRequest,
            UnauthorizedException => (int)HttpStatusCode.Unauthorized,
            NotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var exceptionType = exception switch
        {
            BadRequestException => ProblemTypes.BadRequest,
            UnauthorizedException => ProblemTypes.Unauthorized,
            NotFoundException => ProblemTypes.NotFound,
            _ => ProblemTypes.InternalServerError
        };

        // Create and populate ProblemDetails object
        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred",
            Detail = exception.Message,
            Type = exceptionType,
            Status = statusCode
        };

        // Write the problem details to the response
        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);
        return true;
    }
}