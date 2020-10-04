using AutoMapper;
using Business.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Helper.Startup.DI
{
    public class DICore
    {
        public static void Startup(IServiceCollection services)
        {
            //General
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            //Mapper
            services.AddSingleton(new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }).CreateMapper());
        }
    }
}
