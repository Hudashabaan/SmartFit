using System.Net;
using System.Text.Json;
using SmartFit.Application.Common.Exceptions;
using SmartFit.Application.Common.Models;

namespace SmartFit.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(
                    context,
                    ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            HttpStatusCode statusCode;

            var response =
                new ApiResponse<string>();

            switch (exception)
            {
                case NotFoundException:

                    statusCode =
                        HttpStatusCode.NotFound;

                    response.Message =
                        exception.Message;

                    break;

                case BadRequestException:

                    statusCode =
                        HttpStatusCode.BadRequest;

                    response.Message =
                        exception.Message;

                    break;

                default:

                    statusCode =
                        HttpStatusCode
                            .InternalServerError;

                    response.Message =
                        exception.InnerException?.Message
                        ?? exception.Message;

                    response.Errors =
                        new List<string>
                        {
                            exception.StackTrace
                            ?? "No StackTrace"
                        };

                    break;
            }

            response.Success = false;

            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)statusCode;

            var result =
                JsonSerializer.Serialize(response);

            await context.Response
                .WriteAsync(result);
        }
    }
}