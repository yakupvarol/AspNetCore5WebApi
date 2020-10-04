using WebApi.Helper.ReturnMessage;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WebApi.Helper.Middleware
{
    public class ApiKeyValidatorsMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyValidatorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                return;
            }
            else if (!context.Request.Headers.Keys.Contains("baseapikey") && !context.Request.Path.StartsWithSegments("/swagger"))
            {
                var json = JsonConvert.SerializeObject((new ReturnError { Code = 400, Message = "Bad Request", InternalMessage = "Api Key is missing" }));
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
                return;
            }
            else
            {
                //if (!ContactsRepo.CheckValidUserKey(context.Request.Headers["user-key"]))
                if (context.Request.Headers["baseapikey"] != "test")
                {
                    var json = JsonConvert.SerializeObject((new ReturnError { Code = 401, Message = "UnAuthorized", InternalMessage = "Invalid User Key" }));
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
                    return;
                }
            }
            await _next.Invoke(context);
        }
    }
}
