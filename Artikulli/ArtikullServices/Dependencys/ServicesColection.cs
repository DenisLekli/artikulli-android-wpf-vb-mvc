

using ArtikullServices.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ArtikullServices.Dependencys
{
    public static class ServicesColection
    {
        public static IServiceCollection AddServicesColection(this IServiceCollection service)
        {
            service.AddScoped<ProduktService>();
            service.AddAutoMapper(
    Assembly.GetAssembly(typeof(Class1)));
            return service;
        }
    }
}
