using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Helper.Startup
{
    public class AddCorsServices
    {
        public static void Startup(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://testsitesi.com")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
    }
}
