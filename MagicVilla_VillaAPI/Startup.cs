using AutoMapper;
using MagicVilla_VillaAPI.DependencyInjection;
using Microsoft.OpenApi.Models;
using SkillCheck.Middleware;

namespace MagicVilla_VillaAPI
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }
        public void ConfigureService(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MagicVilla API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Logesh",
                        Email= "logusham1999@gmail.com"
                    },
                });
            });
            services
                .AddDataBase(configuration)
                .AddDomainService()
                .AddAutoMapper(typeof(Profile));
        }
        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //app.UseCors();

            app.UseHttpsRedirection();

            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseCustomExceptionMiddleware();

            app
               .UseRouting()
               .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
