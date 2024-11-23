using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Extensions;
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

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
