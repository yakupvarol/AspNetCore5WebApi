using WebApi.Helper.Filter;
using WebApi.Helper.Startup;
using WebApi.Helper.Startup.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AspNetCoreRateLimit;
using WebApi.Helper.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add<ValidateModelAttribute>()).AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include; options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; });
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            //AddCorsServices.Startup(services);
            CompressionServices.Startup(services);
            MvcServices.Startup(services);
            SwaggerServices.Startup(services);
            DICore.Startup(services);
            DIBusinessDataAccess.Startup(services);
            ApiVersionServices.Startup(services);
            RateLimitServices.Startup(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseIpRateLimiting();
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            //UseConfigure.Headers(app);
            app.UseApiKeyValidatorsMiddleware();
            app.UseExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
