using MagicVilla_Service.IService;
using MagicVilla_Service;
using MagicVilla_Entity.Helper;

namespace MagicVilla_Web.DependencyInjuction
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            services.AddHttpClient<IVillaService, VillaService>();
            services.AddScoped<IVillaService, VillaService>();

            services.AddHttpClient<IVillaNumberService, VillaNumberService>();
            services.AddScoped<IVillaNumberService, VillaNumberService>();

            services.AddHttpClient<IAuthService, AuthService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
        public static IServiceCollection AddConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceUrls>(configuration.GetSection("ServiceUrls"));

            return services;
        }
    }
}
