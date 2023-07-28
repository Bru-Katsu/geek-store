using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace GeekStore.WebApi.Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (OperationCanceledException ex)
            {
                await HandleOperationCanceledExceptionAsync(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            string jsonString = JsonConvert.SerializeObject(new { message = exception.Message });

            await context.Response.WriteAsync(jsonString, Encoding.UTF8);
        }

        private async Task HandleOperationCanceledExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = 444;
            context.Response.ContentType = "application/json";

            string jsonString = JsonConvert.SerializeObject(new { message = "Requisição cancelada!" });

            await context.Response.WriteAsync(jsonString, Encoding.UTF8);
        }
    }
}
