using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using System.Security.Claims;
using TechStore.Core.Exceptions;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Laptop;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;

namespace TechStore.Web.Controllers
{
    [Authorize]
    public class LaptopController : Controller
    {
        private readonly ILaptopService laptopService;
        private readonly IClientService clientService;
        private readonly IUserService userService;

		public LaptopController(ILaptopService laptopService, IClientService clientService, IUserService userService)
        {
            this.laptopService = laptopService;
            this.clientService = clientService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllLaptopsQueryModel query)
        {
	        var result = await this.laptopService.GetAllLaptopsAsync(
		        query.Cpu,
		        query.Ram,
		        query.SsdCapacity,
		        query.VideoCard,
		        query.Keyword,
		        query.Sorting,
                query.CurrentPage);

            query.TotalLaptopsCount = result.TotalLaptopsCount;

            query.Cpus = await this.laptopService.GetAllCpusNames();
	        query.Rams = await this.laptopService.GetAllRamsValues();
	        query.SsdCapacities = await this.laptopService.GetAllSsdCapacitiesValues();
	        query.VideoCards = await this.laptopService.GetAllVideoCardsNames();

            query.Laptops = result.Laptops;

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string information)
        {
            try
            {
                var laptop = await this.laptopService.GetLaptopByIdAsLaptopDetailsExportViewModelAsync(id);

                if (information != laptop.GetInformation())
                {
                    return NotFound();
                }
                
                return View(laptop);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
        
        [HttpGet]
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

                TempData[TempDataMessage] = ProductSuccessfullyDeleted;

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
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
                catch (TechStoreException)
                {
	                return View("AppError");
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

                TempData[TempDataMessage] = ProductSuccessfullyAdded;

                return RedirectToAction(nameof(Details), new { id, information = model.GetInformation() });
            }
            catch (TechStoreException)
            {
	            return View("AppError");
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

                TempData[TempDataMessage] = ProductSuccessfullyEdited;

                return RedirectToAction(nameof(Details), new { id, information = model.GetInformation() });
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = BestUser)]
        public async Task<IActionResult> Mine()
        {
            var userId = this.User.Id();

            try
            {
                var userLaptops = await this.laptopService.GetUserLaptopsAsync(userId);
                return View(userLaptops);
            }
            catch (TechStoreException)
            {
	            return View("AppError");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Buy(int id)
        {
	        if (this.User.IsInRole(Administrator))
	        {
				return Unauthorized();
			}

	        try
	        {
		        var userId = this.User.Id();
		        if (this.User.IsInRole(BestUser))
		        {
			        var laptopSeller = (await this.laptopService.GetLaptopByIdAsLaptopEditViewModelAsync(id)).Seller;
			        if (laptopSeller is not null && laptopSeller.UserId == userId)
			        {
				        return Unauthorized();
			        }
		        }


				ViewData["Title"] = "Buy a Laptop";
		        
				await this.laptopService.MarkLaptopAsBought(id);
		        
		        var client = await this.clientService.BuyProduct(userId);

				var isNowPromotedToBestUser = await this.userService.ShouldBePromotedToBestUser(client);

				if (isNowPromotedToBestUser)
				{
					return View("PromoteToBestUser");
				}

				return View("PurchaseMade");
			}
	        catch (ArgumentException)
	        {
				return NotFound();
			}
        }
	}
}
