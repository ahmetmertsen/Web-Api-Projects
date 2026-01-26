using ECommerceAPI.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ECommerceAPI.WebAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);

            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode;
            string message;

            switch (ex)
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = ex.Message;
                    break;
                case ValidationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;
                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = ex.Message;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = ex.Message;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new 
            {
                StatusCode = statusCode,
                Message = message
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
