using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Laptop;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Web.Controllers
{
    [Authorize]
    public class LaptopController : Controller
    {
        private readonly ILaptopService laptopService;

        public LaptopController(ILaptopService laptopService)
        {
            this.laptopService = laptopService;
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
                var laptop = await this.laptopService.GetLaptopByIdAsDtoAsync(id);
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
                var laptop = await this.laptopService.GetLaptopByIdAsDtoAsync(id);

                if (this.User.IsInRole(BestUser) && this.User.Id() != laptop.SellerId)
                {
                    return BadRequest();
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
        public IActionResult Add()
        {
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
    }
}
