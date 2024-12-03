using Microsoft.AspNetCore.Identity;
using TechStore.Core.Contracts;
using TechStore.Infrastructure.Data.Models;
using TechStore.Infrastructure.Data.Models.Account;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Core.Services
{
    public class UserService : IUserService
    {
	    private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<bool> ShouldBePromotedToBestUser(Client client)
        {
	        if (client.CountOfPurchases == RequiredNumberOfPurchasesToBeBestUser)
	        {
				var user = await this.userManager.FindByIdAsync(client.UserId);
				await this.userManager.AddToRoleAsync(user, BestUser);
				await this.signInManager.SignOutAsync();
				await this.signInManager.SignInAsync(user, false);
				return true;
			}

	        return false;
		}
	}
}
