using WebApi.Helper.ReturnMessage;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WebApi.Helper.Middleware
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            //
            var InnerException = "";
            if (ex.InnerException != null)
                InnerException = ex.InnerException.Message;

            //
            var json = JsonConvert.SerializeObject((new ReturnError { InternalMessage = InnerException, Message = ex.Message }));
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(json);
        }
    }
}
