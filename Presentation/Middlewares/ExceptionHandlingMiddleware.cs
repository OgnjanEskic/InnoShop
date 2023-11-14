using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Presentation.Middlewares
{
    /// <summary>
    /// Represents the middleware class to catch exceptions.
    /// </summary>
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Constructor for ExceptionHandlingMiddleware class.
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Invokes the exception handling middleware.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <param name="next">The RequestDelegate.</param>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server error.", ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problemDetails = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error.",
                    Detail = "An internal server error occurred."
                };
                string json = JsonSerializer.Serialize(problemDetails);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}
