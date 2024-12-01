using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Keyboard;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using System.Security.Claims;
using TechStore.Core.Exceptions;

namespace TechStore.Web.Controllers
{
    [Authorize]
    public class KeyboardController : Controller
    {
        private readonly IKeyboardService keyboardService;
        private readonly IClientService clientService;
        private readonly IUserService userService;

		public KeyboardController(IKeyboardService keyboardService, IClientService clientService, IUserService userService)
        {
            this.keyboardService = keyboardService;
            this.clientService = clientService;
			this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllKeyboardsQueryModel query)
        {
            var result = await this.keyboardService.GetAllKeyboardsAsync(query.Format, query.Type, query.Wireless,
                query.Keyword, query.Sorting, query.CurrentPage);

            query.TotalKeyboardsCount = result.TotalKeyboardsCount;

            query.Formats = await this.keyboardService.GetAllKeyboardsFormatsAsync();
            query.Types = await this.keyboardService.GetAllKeyboardsTypesAsync();

            query.Keyboards = result.Keyboards;

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string information)
        {
            try
            {
                var keyboard = await this.keyboardService.GetKeyboardByIdAsKeyboardDetailsExportViewModelAsync(id);

                if (information.ToLower() != keyboard.GetInformation().ToLower())
                {
                    return NotFound();
                }

                return View(keyboard);
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
                var keyboard = await this.keyboardService.GetKeyboardByIdAsKeyboardDetailsExportViewModelAsync(id);

                if (this.User.IsInRole(BestUser) && (keyboard.Seller is null || this.User.Id() != keyboard.Seller.UserId))
                {
                    return Unauthorized();
                }

                await this.keyboardService.DeleteKeyboardAsync(id);
                
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
						ViewData["Title"] = "Add a Keyboard";

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
        public async Task<IActionResult> Add(KeyboardImportViewModel model, bool? radioButton)
        {
	        if (radioButton is null)
	        {
				this.ModelState.AddModelError("IsWireless", ErrorMessageForUnselectedOption);
			}
	        else
	        {
				model.IsWireless = (bool)radioButton;
			}

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
		        int id = await this.keyboardService.AddKeyboardAsync(model, userId);

		        TempData[TempDataMessage] = ProductSuccessfullyAdded;

		        return RedirectToAction(nameof(Details), new { id, information = model.GetInformation() });
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
		        var keyboard = await this.keyboardService.GetKeyboardByIdAsKeyboardEditViewModelAsync(id);

		        if (this.User.IsInRole(BestUser)
		            && (keyboard.Seller is null || this.User.Id() != keyboard.Seller.UserId))
		        {
					return Unauthorized();
				}

		        return View(keyboard);
	        }
	        catch (ArgumentException)
	        {
		        return NotFound();
	        }
        }

        [HttpPost]
        [Authorize(Roles = $"{Administrator}, {BestUser}")]
        public async Task<IActionResult> Edit(KeyboardEditViewModel model)
        {
	        if (!this.ModelState.IsValid)
	        {
		        return View();
	        }

	        try
	        {
		        var keyboard = await this.keyboardService.GetKeyboardByIdAsKeyboardEditViewModelAsync(model.Id);

		        if (this.User.IsInRole(BestUser)
		            && (keyboard.Seller is null || this.User.Id() != keyboard.Seller.UserId))
		        {
					return Unauthorized();
				}

		        int id = await this.keyboardService.EditKeyboardAsync(model);

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
		        var userKeyboards = await this.keyboardService.GetUserKeyboardsAsync(userId);

		        return View(userKeyboards);
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
					var keyboardSeller = (await this.keyboardService.GetKeyboardByIdAsKeyboardEditViewModelAsync(id)).Seller;

					if (keyboardSeller is not null && keyboardSeller.UserId == userId)
					{
						return Unauthorized();
					}
				}

		        ViewData["Title"] = "Buy a Keyboard";

		        await this.keyboardService.MarkKeyboardAsBoughtAsync(id);

		        var client = await this.clientService.BuyProduct(userId);

		        var isNowPromotedToBestUser = await this.userService.ShouldBePromotedToBestUser(client);

		        if (isNowPromotedToBestUser)
		        {
					return View(PromoteToBestUserViewName);
				}

		        return View(PurchaseMadeViewName);
			}
			catch (ArgumentException)
			{
				return NotFound();
			}
        }
	}
}
