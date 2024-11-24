using Microsoft.Extensions.DependencyInjection;
using TechStore.Core.Contracts;
using TechStore.Core.Services;
using TechStore.Infrastructure.Common;


namespace TechStore.Core.Extensions
{
    public static class TechStoreServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ILaptopService, LaptopService>();
            services.AddScoped<IClientService, ClientService>();
            return services;
        }
    }
}
