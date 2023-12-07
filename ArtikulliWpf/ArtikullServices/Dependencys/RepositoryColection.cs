
using ArtikullServices.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ArtikullServices.Dependencys
{
    public static class RepositoryColection
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection service)
        {
            service.AddScoped<ProduktRepository>();
            return service;
        }
    }
}
