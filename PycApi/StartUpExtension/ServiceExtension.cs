using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PycApi.Mapper;
using PycApi.Service;
using PycApi.Service.Concrete;

namespace PycApi.StartUpExtension
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            // services 
            services.AddScoped<IVehicleService,VehicleService>();
            services.AddScoped<IContainerService,ContainerService>();


            // mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
