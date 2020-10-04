using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Helper.Startup
{
    public class MvcServices
    {
        public static void Startup(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ConsumesAttribute("application/json"));
            });
        }
    }
}
