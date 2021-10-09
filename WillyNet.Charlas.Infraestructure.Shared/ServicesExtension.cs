using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Infraestructure.Shared.Services;
using WillyNet.Charlas.Infraestructure.Shared.Services.AzureServices;
using WillyNet.Charlas.Infraestructure.Shared.Settings;

namespace WillyNet.Charlas.Infraestructure.Shared
{
    public static class ServicesExtension
    {
        public static void AddSharedInfraestructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();

            var blobStorageSettings = new BlobStorageSettings();
            _config.GetSection(BlobStorageSettings.SettingName).Bind(blobStorageSettings);

            services.AddSingleton(x => new BlobServiceClient(blobStorageSettings.ConnectionString));

            services.AddScoped<IFileStorageService, BlobStorageService>();
        }
    }
}
