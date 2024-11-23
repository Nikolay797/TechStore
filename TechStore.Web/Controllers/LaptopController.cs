using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;

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
                var laptop = await this.laptopService.GetLaptopByIdAsync(id);

                return View(laptop);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
