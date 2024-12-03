using Microsoft.Extensions.DependencyInjection;
using TechStore.Core.Contracts;
using TechStore.Core.Exceptions;
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITelevisionService, TelevisionService>();
            services.AddScoped<IKeyboardService, KeyboardService>();
            services.AddScoped<IMouseService, MouseService>();
            services.AddScoped<IHeadphoneService, HeadphoneService>();
            services.AddScoped<ISmartwatchService, SmartwatchService>();
            services.AddScoped<IAdminUserService, AdminUserService>();

			services.AddScoped<IGuard, Guard>();
			return services;
        }
    }
}
