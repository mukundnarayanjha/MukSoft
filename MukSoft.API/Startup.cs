using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MukSoft.API.Extensions;
using Serilog;
using System.Reflection;

namespace MukSoft.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            env.EnvironmentName = "Local";
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureMSSqlContext(Configuration);
            services.ConfigureRepository();
            services.AddAutoMapper();
            services.ConfigureModelStateValidation();
            services.ConfigureCacheAttribute();
            services.ConfigureSwagger();
            // services.ConfigureJwtAuthentication();

            //Add MediatR
            services.AddMediatR();
            services.AddTransient<IMediator, Mediator>();
            services.AddMediatorHandlers(typeof(Startup).GetTypeInfo().Assembly);
            services.AddMediatorHandlers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            #region Anti Forgery Tokens
            //app.UseAntiforgeryTokens();
            #endregion

            #region Added Custom Error Handling Middleware.
            app.UseErrorLogging();
            loggerFactory.AddSerilog();
            #endregion

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            #region Use Authentication
            // app.UseAuthentication();
            #endregion

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });
        }
    }
}
