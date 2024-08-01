using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Innowise.Common.Exceptions;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, ProblemDetailsFactory problemDetailsFactory) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
                                                CancellationToken cancellationToken)
    {
        logger.LogError("Error occurred: {Message}", exception.Message);

        var statusCode = exception switch
        {
            BadRequestException => (int) HttpStatusCode.BadRequest,
            UnauthorizedException => (int) HttpStatusCode.Unauthorized,
            NotFoundException => (int) HttpStatusCode.NotFound,
            _ => (int) HttpStatusCode.InternalServerError
        };

        var problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext, statusCode, title: exception.Message, instance: httpContext.Request.Path);

        // var exceptionType = exception switch
        // {
        //     BadRequestException => ProblemTypes.BadRequest,
        //     UnauthorizedException => ProblemTypes.Unauthorized,
        //     NotFoundException => ProblemTypes.NotFound,
        //     _ => ProblemTypes.InternalServerError
        // };

        // Create and populate ProblemDetails object
        // var problemDetails = new ProblemDetails
        // {
        //     Title = "An error occurred",
        //     Detail = exception.Message,
        //     Type = exceptionType,
        //     Status = statusCode
        // };

        // Write the problem details to the response
        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);
        return true;
    }
}