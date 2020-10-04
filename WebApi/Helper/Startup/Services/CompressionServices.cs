using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace WebApi.Helper.Startup
{
    public class CompressionServices
    {
        public static void Startup(IServiceCollection services)
        {
            services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });
            services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
        }
    }
}