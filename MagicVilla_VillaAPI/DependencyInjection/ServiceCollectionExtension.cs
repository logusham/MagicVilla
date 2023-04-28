using MagicVilla_VillaAPI.Logger;
using MagicVilla_VillaAPI.Repository.IRepository;
using MagicVilla_VillaAPI.Repository;
using MagicVilla_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services,IConfiguration configuration)
        {
            AddDataBase(services, configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            services.AddSingleton<ILogging, Logging>()
            .AddScoped<IVillaRepository, VillaRepository>()
            .AddScoped<IVillaNumberRepository, VillaNumberRepository>();
            return services;
        }
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var isProduction = configuration.GetValue<bool>("InProduction");
            if(isProduction)
            {
                services.AddDbContext<ApplicationDbContext>(option =>
                {
                    option.UseSqlServer(configuration.GetConnectionString(""));
                });
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(option =>
                {
                    option.UseSqlServer(configuration.GetConnectionString(""));
                });
            }
            return services;
        }
    }
}
