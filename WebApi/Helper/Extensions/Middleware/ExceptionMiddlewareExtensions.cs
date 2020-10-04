using WebApi.Helper.Middleware;
using Microsoft.AspNetCore.Builder;

namespace WebApi.Helper.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
