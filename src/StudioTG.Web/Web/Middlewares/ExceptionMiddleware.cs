using FluentValidation;
using Serilog;
using StudioTG.Application.DTO.Responses;
using System.Net;
using System.Text;
using System.Text.Json;

namespace StudioTG.Web.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            ErrorResponse response;

            if (exception is ValidationException validationException)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var error in validationException.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }
                response = new ErrorResponse
                {
                    Error = stringBuilder.ToString()
                };
            }
            else if (exception is OperationCanceledException)
            {
                context.Response.StatusCode = 499;
                response = new ErrorResponse
                {
                    Error = "Запрос был отменён клиентом"
                };
            }
            else
            {
                response = new ErrorResponse
                {
                    Error = exception.Message
                };
            }
            Log.Error(exception, "");
            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
