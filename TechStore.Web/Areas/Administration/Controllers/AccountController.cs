using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Infrastructure.Data.Models.Account;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = $"{Administrator}")]
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IUserService userService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var role = this.roleManager.Roles.FirstOrDefault(r => r.Name == Administrator);
            
            var users = await this.userService.GetAllUsersThatAreNotInTheSpecifiedRole(role?.Id ?? null);
            
            return View("~/Areas/Administration/Views/Account/GetUsers.cshtml", users);
        }

        [HttpGet]
        public async Task<IActionResult> PromoteToAdmin(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }
            await this.userManager.AddToRoleAsync(user, Administrator);
            
            return View("~/Areas/Administration/Views/Account/PromoteToAdmin.cshtml", user);
        }
    }
}
