using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MukSoft.Core.Context;
using MukSoft.Data.Interfaces;
using MukSoft.Data.Services;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MukSoft.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    );
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
        public static void ConfigureMSSqlContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opts => opts
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b
                    .MigrationsAssembly("MukSoft.API")));
        }

        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IUserService, UserService>();
        }
        public static void ConfigureModelStateValidation(this IServiceCollection services)
        {
            services.AddMvc(config =>
                  config.Filters.Add(new ModelStateValidationFilter()))
                 .AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; });
        }
        public static void ConfigureCacheAttribute(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ResponseCacheAttribute() { Duration = 0, NoStore = true, Location = ResponseCacheLocation.None });
            });
            services.AddResponseCaching();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Test API", Version = "v1" });
            });
        }

        #region Jwt Authentication
        public static void ConfigureJwtAuthentication(this IServiceCollection services)
        {
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "http://muksoft.com",

                ValidateAudience = true,
                ValidAudience = "http://muksoft.com",

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("condimentumvestibulumSuspendissesitametpulvinarorcicondimentummollisjusto")),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParams;
            });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }
        #endregion

    }
}
