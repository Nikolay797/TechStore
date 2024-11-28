using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TechStore.Infrastructure.Data.Models.Account;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using static TechStore.Infrastructure.Constants.DataConstant.UserConstants;

namespace TechStore.Core.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
		{
			using var scopedServices = app.ApplicationServices.CreateScope();
			
			var services = scopedServices.ServiceProvider;
			
			var userManager = services.GetRequiredService<UserManager<User>>();
			
			var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

			Task.Run(async () =>
			{
				if (!(await roleManager.RoleExistsAsync(Administrator)))
				{
					await roleManager.CreateAsync(new IdentityRole { Name = Administrator });
				}

				var admin = await userManager.FindByNameAsync(AdminUserName);
				
				await userManager.AddToRoleAsync(admin, Administrator);
			})

			.GetAwaiter()
			.GetResult();

			return app;
		}

		public static IApplicationBuilder SeedBestUser(this IApplicationBuilder app)
		{
			using var scopedServices = app.ApplicationServices.CreateScope();
			
			var services = scopedServices.ServiceProvider;
			
			var userManager = services.GetRequiredService<UserManager<User>>();
			
			var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

			Task.Run(async () =>
			{
				if (!(await roleManager.RoleExistsAsync(BestUser)))
				{
					await roleManager.CreateAsync(new IdentityRole { Name = BestUser });
				}

				var bestUser = await userManager.FindByNameAsync(BestUserUserName);
				
				await userManager.AddToRoleAsync(bestUser, BestUser);
			})
			.GetAwaiter()
			.GetResult();

			return app;
		}
	}
}
