using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Interfaces.Utilities;
using WillyNet.Charlas.Core.Domain.Entities;
using WillyNet.Charlas.Core.Domain.Settings;
using WillyNet.Charlas.Infraestructure.Persistence.Contexts;
using WillyNet.Charlas.Infraestructure.Persistence.Repository;
using WillyNet.Charlas.Infraestructure.Persistence.Services;
using WillyNet.Charlas.Infraestructure.Persistence.Services.Utilities;

namespace WillyNet.Charlas.Infraestructure.Persistence
{
    public static class ServicesExtension
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region CONTEXTS
            services.AddDbContext<DbCharlaContext>(
                   options => options.UseSqlServer(
                         configuration.GetConnectionString("DefaultConnection"),
                         b => b.MigrationsAssembly(typeof (DbCharlaContext).Assembly.FullName)
                  )
                );
            #endregion

            #region IDENTITY
            services.AddIdentity<UserApp, IdentityRole>()
                    .AddEntityFrameworkStores<DbCharlaContext>()
                    .AddDefaultTokenProviders();

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                //.AddCookie
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                    };

                    /*
                    En las API web estándar, los tokens de portador se envían en un encabezado HTTP. 
                    Sin embargo, SignalR no puede configurar estos encabezados en los navegadores cuando 
                    se utilizan algunos transportes.
                    Cuando se utilizan WebSockets y Eventos enviados por el servidor, el token se transmite 
                    como un parámetro de cadena de consulta.
                     */
                    
                    o.Events = new JwtBearerEvents
                    {
                        
                        OnMessageReceived = context =>
                        {
                            //para pasar el token por query string
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notify"))                            
                                context.Token = accessToken;
                            
                            return Task.CompletedTask;
                        },

                        /*
                         OnAuthenticationFailed = c =>
                         {
                             c.NoResult();
                             c.Response.StatusCode = 500;
                             c.Response.ContentType = "text/plain";
                             return c.Response.WriteAsync(c.Exception.ToString());
                         },
                        OnChallenge = context =>
                        {
                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                context.HandleResponse();
                                var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                                return context.Response.WriteAsync(result);
                            }
                            else
                            {
                                var result = JsonConvert.SerializeObject(new Response<string>("Token Expired"));
                                return context.Response.WriteAsync(result);
                            }
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                            return context.Response.WriteAsync(result);
                        },*/
                    };

                });
            #endregion

            #region REPOSITORIES
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            #endregion

            #region SERVICES
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransactionDb, TransactionDb>();
            #endregion

            #region UTILITIES
            services.AddTransient<IControlUtil, ControlUtil>();
            #endregion

        }
    }
}
