using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using TechStore.Core.Models.User;
using TechStore.Infrastructure.Data.Models.Account;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var model = new SignUpViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn(string? returnUrl = null)
        {
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var model = new SignInViewModel();
            {
                returnUrl = returnUrl;
            };
            
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user is not null)
            {
                var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    if (model.ReturnUrl is not null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            this.ModelState.AddModelError("", "Invalid sign in attempt!");
            return View(model);
        }

        public async Task<IActionResult> Signout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> AddUsersToRolesInitial()
        {
            var adminUser = await this.userManager.FindByNameAsync("admin");

            await this.userManager.AddToRoleAsync(adminUser, Administrator);

            var bestUser = await this.userManager.FindByNameAsync("bestUser");

            await this.userManager.AddToRoleAsync(bestUser, BestUser);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}