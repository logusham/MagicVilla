using MagicVilla_VillaAPI.DependencyInjection;
using MagicVilla_VillaAPI.Mapper;
using Microsoft.OpenApi.Models;
using SkillCheck.Middleware;

namespace MagicVilla_VillaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MagicVilla API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Logesh",
                        Email = "logusham1999@gmail.com"
                    },
                });
            });
            builder.Services.AddDataBase(builder.Configuration)
                .AddAutoMapper(typeof(Profiles).Assembly)
                .AddDomainService();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCustomExceptionMiddleware();

            app.MapControllers();

            app.Run();

        }
    }
}


