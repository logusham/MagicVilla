using MagicVilla_DataRepository.DataRepository.Implementation;
using MagicVilla_DataRepository.DataRepository.Interface;
using MagicVilla_VillaAPI.Data;
//using MagicVilla_VillaAPI.Logger;
using MagicVilla_VillaAPI.Repository;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services,IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            //services.AddSingleton<ILogging, Logging>();

            services.AddScoped<IVillaRepository, VillaRepository>();
            services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var isProduction = configuration.GetValue<bool>("InProduction");
            if (isProduction)
            {
                services.AddDbContext<ApplicationDbContext>(option =>
                {
                    option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(option =>
                {
                    option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });
            }
            return services;
        }
    }
}
