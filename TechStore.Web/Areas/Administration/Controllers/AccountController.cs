using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TechStore.Core.Contracts;
using TechStore.Core.Exceptions;
using static TechStore.Web.Areas.Administration.Constant;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using TechStore.Core.Models.User;

namespace TechStore.Web.Areas.Administration.Controllers
{
    public class AccountController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;

        public AccountController(RoleManager<IdentityRole> roleManager, IUserService userService, IMemoryCache memoryCache)
        {
            this.roleManager = roleManager;
            this.userService = userService;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = this.memoryCache.Get<IEnumerable<UserExportViewModel>>(UsersCacheKey);

            if (users is null)
            {
                var role = this.roleManager.Roles.FirstOrDefault(r => r.Name == Administrator);
                users = await this.userService.GetAllUsersThatAreNotInTheSpecifiedRole(role?.Id ?? null);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(3));

                this.memoryCache.Set(UsersCacheKey, users, cacheOptions);
            }

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> PromoteToAdmin(string id)
        {
            try
            {
                this.memoryCache.Remove(UsersCacheKey);

                return View();
            }
            catch (TechStoreException)
            {
                return NotFound();
            }
        }
    }
}