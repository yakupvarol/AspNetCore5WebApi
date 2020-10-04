using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Helper.Startup.DI
{
    public class DIBusinessDataAccess
    {
        public static void Startup(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDal, EfUserDal>();
        }
    }
}
