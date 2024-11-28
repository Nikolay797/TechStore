using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TechStore.Core.Contracts;
using TechStore.Core.Exceptions;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Television;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Web.Controllers
{
    [Authorize]
    public class TelevisionController : Controller
    {
        private readonly ITelevisionService televisionService;
        private readonly IClientService clientService;
        private readonly IUserService userService;

		public TelevisionController(ITelevisionService televisionService, IClientService clientService, IUserService userService)
        {
            this.televisionService = televisionService;
            this.clientService = clientService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllTelevisionsQueryModel query)
        {
            var result = await this.televisionService.GetAllTelevisionsAsync(
                query.Brand,
                query.DisplaySize,
                query.Resolution,
                query.Keyword,
                query.Sorting,
                query.CurrentPage);

            query.TotalTelevisionsCount = result.TotalTelevisionsCount;
            query.Brands = await this.televisionService.GetAllBrandsNames();
            query.DisplaySizes = await this.televisionService.GetAllDisplaysSizesValues();
            query.Resolutions = await this.televisionService.GetAllResolutionsValues();
            query.Televisions = result.Televisions;

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var television = await this.televisionService
                    .GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync(id);

                return View(television);
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
                var television = await this.televisionService
                    .GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync(id);

                if (this.User.IsInRole(BestUser) &&
                    (television.Seller is null || this.User.Id() != television.Seller.UserId))
                {
                    return Unauthorized();
                }

                await this.televisionService.DeleteTelevisionAsync(id);

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
                        ViewData["Title"] = "Add a Television";

                        return View("AddNotAllowed");
                    }
                }
                catch (TechStoreException)
                {
                    return View("Error");
                }
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{Administrator}, {BestUser}")]
        public async Task<IActionResult> Add(TelevisionImportViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            string? userId = null;

            if (this.User.IsInRole(BestUser))
            {
                userId = this.User.Id();
            }

            try
            {
                int id = await this.televisionService.AddTelevisionAsync(model, userId);

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (TechStoreException)
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{Administrator}, {BestUser}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var television = await this.televisionService
                    .GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync(id);

                if (this.User.IsInRole(BestUser) &&
                    (television.Seller is null || this.User.Id() != television.Seller.UserId))
                {
                    return Unauthorized();
                }

                return View(television);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = $"{Administrator}, {BestUser}")]
        public async Task<IActionResult> Edit(TelevisionEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var television = await this.televisionService
                    .GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync(model.Id);

                if (this.User.IsInRole(BestUser) &&
                    (television.Seller is null || this.User.Id() != television.Seller.UserId))
                {
                    return Unauthorized();
                }

                int id = await this.televisionService.EditTelevisionAsync(model);

                return RedirectToAction(nameof(Details), new { id });
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
		        var userTelevisions = await this.televisionService.GetUserTelevisionsAsync(userId);

		        return View(userTelevisions);
			}
	        catch (TechStoreException)
	        {
		        return View("Error");
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
					var televisionSeller = (await this.televisionService.GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync(id)).Seller;

					if (televisionSeller is not null && televisionSeller.UserId == userId)
					{
						return Unauthorized();
					}
				}

		        ViewData["Title"] = "Buy a Television";
		        await this.televisionService.MarkTelevisionAsBought(id);
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
