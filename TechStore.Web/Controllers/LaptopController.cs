using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Laptop;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;

namespace TechStore.Web.Controllers
{
    [Authorize]
    public class LaptopController : Controller
    {
        private readonly ILaptopService laptopService;
        private readonly IClientService clientService;

        public LaptopController(ILaptopService laptopService, IClientService clientService)
        {
            this.laptopService = laptopService;
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var laptops = await this.laptopService.GetAllLaptopsAsync();
            return View(laptops);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var laptop = await this.laptopService.GetLaptopByIdAsLaptopDetailsExportViewModelAsync(id);
                return View(laptop);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = $"{Administrator}, {BestUser}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var laptop = await this.laptopService.GetLaptopByIdAsLaptopDetailsExportViewModelAsync(id);

                if (this.User.IsInRole(BestUser)
                    && (laptop.Seller is null || this.User.Id() != laptop.Seller.UserId))
                {
                    return Unauthorized();
                }

                await this.laptopService.DeleteLaptopAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{Administrator}, {BestUser}")]
        public async Task<IActionResult> Add()
        {
            if (this.User.IsInRole(BestUser))
            {
                var userId = this.User.Id();

                try
                {
                    var numberOfActiveSales = await this.clientService.GetNumberOfActiveSales(userId);

                    if (numberOfActiveSales == MaxNumberOfAllowedSales)
                    {
                        ViewData["Title"] = "Add a Laptop";

                        return View("AddNotAllowed");
                    }
                }
                catch (Exception)
                {
                    return Unauthorized();
                }
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{Administrator}, {BestUser}")]
        public async Task<IActionResult> Add(LaptopImportViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string? userId = null;

                if (this.User.IsInRole(BestUser))
                {
                    userId = this.User.Id();
                }

                int id = await this.laptopService.AddLaptopAsync(model, userId);
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong... :)");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{Administrator}, {BestUser}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var laptop = await this.laptopService.GetLaptopByIdAsLaptopEditViewModelAsync(id);

                if (this.User.IsInRole(BestUser)
                    && (laptop.Seller is null || this.User.Id() != laptop.Seller.UserId))
                {
                    return Unauthorized();
                }

                return View(laptop);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = $"{Administrator}, {BestUser}")]
        public async Task<IActionResult> Edit(LaptopEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var laptop = await this.laptopService.GetLaptopByIdAsLaptopEditViewModelAsync(model.Id);

                if (this.User.IsInRole(BestUser)
                    && (laptop.Seller is null || this.User.Id() != laptop.Seller.UserId))
                {
                    return Unauthorized();
                }

                int id = await this.laptopService.EditLaptopAsync(model);
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = BestUser)]
        public async Task<IActionResult> MyLaptops()
        {
            var userId = this.User.Id();

            try
            {
                var userLaptops = await this.laptopService.GetUserLaptopsAsync(userId);
                return View(userLaptops);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
