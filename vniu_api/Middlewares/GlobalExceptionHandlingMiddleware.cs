using System.Net;
using vniu_api.Exceptions;
using BadRequestException = vniu_api.Exceptions.BadRequestException;
using NotFoundException = vniu_api.Exceptions.NotFoundException;
using ArgumentException = vniu_api.Exceptions.ArgumentException;
using UnauthorizedAccessException = vniu_api.Exceptions.UnauthorizedAccessException;
using System.Text.Json;

namespace vniu_api.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message = "";

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
                //stackTrace = ex.StackTrace;
            } else if (exceptionType == typeof(NotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                //stackTrace = ex.StackTrace;
            } else if (exceptionType == typeof(DuplicateValueException))
            {
                message = ex.Message;
                status = HttpStatusCode.Conflict;
                //stackTrace = ex.StackTrace;
            } else if (exceptionType == typeof(ArgumentException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                //stackTrace = ex.StackTrace;
            } else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = ex.Message;
                status = HttpStatusCode.Unauthorized;
                //stackTrace = ex.StackTrace;
            } else
            {
                message = ex.Message;
                status = HttpStatusCode.InternalServerError;
                //stackTrace = ex.StackTrace;
            }

            var exceptionResult = JsonSerializer.Serialize(new { error = message, status });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
