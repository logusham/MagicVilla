using MagicVilla_Service.IService;
using MagicVilla_Service;

namespace MagicVilla_Web.DependencyInjuction
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            //services.AddHttpClient<IVillaService, VillaService>();
            //services.AddScoped<IVillaService, VillaService>();
            //services.AddHttpClient<IVillaNumberService, VillaNumberService>();
            //services.AddScoped<IVillaNumberService, VillaNumberService>();
            return services;
        }
    }
}
