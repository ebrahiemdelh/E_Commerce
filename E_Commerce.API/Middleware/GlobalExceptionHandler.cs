using E_Commerce.Application.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace E_Commerce.API.Middleware
{
    public class GlobalExceptionHandler(
        ProblemDetailsFactory problemDetailsFactory,
        IHostEnvironment hostEnvironment,
        ILogger<GlobalExceptionHandler> logger
        ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var error = Map(exception);
            var statusCode = error.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError,
            };

            logger.LogError(exception, "An error occurred");
            var problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext, statusCode, error.Code,
                detail: hostEnvironment.IsDevelopment() ? exception.ToString() : error.Description);

            problemDetails.Extensions["error"] = new { error };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, options: null, contentType: "application/prblem+json", cancellationToken);
            return true;
        }

        private static Error Map(Exception exception) => exception switch
        {
            UnauthorizedAccessException accessException => Error.Unauthorized(),
            _ => Error.Failure()
        };
    }
}
