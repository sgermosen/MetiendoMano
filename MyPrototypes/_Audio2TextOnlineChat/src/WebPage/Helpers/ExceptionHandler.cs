using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WebPage.Helpers
{
    /// <summary>
    /// Middleware - error handling
    /// </summary>
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                // customize as you need
                error = new
                {
                    message = exception.Message,
                    exception = exception.GetType().Name
                }
            });
            await response.WriteAsync(result);
            await File.WriteAllTextAsync("internal_error_log", result);
            Debug.Print("ERROR FOUND", result);
        }
    }

}
