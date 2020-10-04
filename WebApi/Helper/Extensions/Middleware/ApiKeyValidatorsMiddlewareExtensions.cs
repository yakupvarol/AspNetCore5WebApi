using WebApi.Helper.Middleware;
using Microsoft.AspNetCore.Builder;

namespace WebApi.Helper.Extensions
{
    public static class ApiKeyValidatorsMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyValidatorsMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiKeyValidatorsMiddleware>();
        }
    }
}
