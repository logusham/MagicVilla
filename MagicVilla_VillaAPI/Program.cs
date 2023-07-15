using MagicVilla_VillaAPI.DependencyInjection;
using MagicVilla_VillaAPI.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SkillCheck.Middleware;
using System.Text;

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
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo
                .File("C:\\Users\\TEMP\\Desktop\\DotNet Project\\MagicVilla\\MagicVilla_VillaAPI\\log\\villaLogs20230328.txt",rollingInterval:RollingInterval.Day).CreateLogger();
            builder.Host.UseSerilog();
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
            var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
            builder.Services.AddDataBase(builder.Configuration)
                .AddAutoMapper(typeof(Profiles).Assembly)
                .AddDomainService();
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCustomExceptionMiddleware();

            app.MapControllers();

            app.Run();

        }
    }
}


