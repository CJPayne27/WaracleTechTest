using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HotelWaracleBookingApi.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            var exceptionMessage = exception.Message;

            logger.LogError(exception, "Error message: {@ExceptionMessage}", exceptionMessage);

            var problemDetails = new ProblemDetails
            {
                Title = "Server Error: " + exceptionMessage,
                Status = StatusCodes.Status500InternalServerError,
                Detail = exceptionMessage,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
