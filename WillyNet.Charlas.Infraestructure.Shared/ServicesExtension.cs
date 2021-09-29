using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Infraestructure.Shared.Services;

namespace WillyNet.Charlas.Infraestructure.Shared
{
    public static class ServicesExtension
    {
        public static void AddSharedInfraestructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}
