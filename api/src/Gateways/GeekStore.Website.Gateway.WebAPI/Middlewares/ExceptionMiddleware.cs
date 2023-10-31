using Newtonsoft.Json;
using Refit;
using System.Net;
using System.Text;

namespace GeekStore.Website.Gateway.WebAPI.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (OperationCanceledException ex)
            {
                await HandleOperationCanceledExceptionAsync(context);
            }
            catch (ApiException ex)
            {
                await HandleRefitExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleRefitExceptionAsync(HttpContext context, ApiException exception)
        {
            context.Response.StatusCode = (int)exception.StatusCode;
            context.Response.ContentType = "application/json";

            string jsonString = exception.HasContent ? 
                exception.Content : 
                JsonConvert.SerializeObject(new { message = "Erro durante comunicação com serviço interno" });

            await context.Response.WriteAsync(jsonString, Encoding.UTF8);
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
