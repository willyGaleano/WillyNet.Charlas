using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Infraestructure.Persistence;
using WillyNet.Charlas.Infraestructure.Shared;
using WillyNet.Charlas.Presentation.WebApi.Extensions;
using WillyNet.Charlas.Presentation.WebApi.Services;

namespace WillyNet.Charlas.Presentation.WebApi
{
    public class Startup
    {
        readonly string myPolicy = "policyApiCharla";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddSharedInfraestructure(Configuration);
            services.AddPersistenceInfraestructure(Configuration);
            services.AddApiVersioningExtension();
            services.AddSwaggerExtension();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddCors(options => options.AddPolicy(myPolicy,
                             builder => builder.WithOrigins(Configuration["Cors:OriginCors"])
                                              .AllowAnyHeader()
                                              .AllowAnyMethod()
                                              .AllowCredentials()
                            ));
            services.AddControllers();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseErrorHandlingMiddleware();          
            app.UseRouting();
            app.UseSwaggerExtension();
            app.UseCors(myPolicy);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
