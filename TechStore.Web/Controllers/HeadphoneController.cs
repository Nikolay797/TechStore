using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Exceptions;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Headphone;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;

namespace TechStore.Web.Controllers
{
	[Authorize]
	public class HeadphoneController : Controller
	{
		private readonly IHeadphoneService headphoneService;
		private readonly IClientService clientService;
		private readonly IUserService userService;

		public HeadphoneController(IHeadphoneService headphoneService, IClientService clientService, IUserService userService)
		{
			this.headphoneService = headphoneService;
			this.clientService = clientService;
			this.userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Index([FromQuery] AllHeadphonesQueryModel query)
		{
			var result = await this.headphoneService.GetAllHeadphonesAsync(
				query.Type,
				query.Wireless,
				query.Keyword,
				query.Sorting,
				query.CurrentPage);

			query.TotalProductsCount = result.TotalHeadphonesCount;
			query.Types = await this.headphoneService.GetAllHeadphonesTypesAsync();
			query.Headphones = result.Headphones;

			return View(query);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id, string information)
		{
			try
			{
				var headphone = await this.headphoneService.GetHeadphoneByIdAsHeadphoneDetailsExportViewModelAsync(id);

				if (information.ToLower() != headphone.GetInformation().ToLower())
				{
					return NotFound();
				}

				return View(headphone);
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
				var headphone = await this.headphoneService.GetHeadphoneByIdAsHeadphoneDetailsExportViewModelAsync(id);

				if (this.User.IsInRole(BestUser)
					&& (headphone.Seller is null || this.User.Id() != headphone.Seller.UserId))
				{
					return Unauthorized();
				}

				await this.headphoneService.DeleteHeadphoneAsync(id);

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
						ViewData["Title"] = "Add a Headphone";

						return View(AddNotAllowedViewName);
					}
				}
				catch (TechStoreException)
				{
					return View(ErrorCommonViewName);
				}

				var model = new HeadphoneImportViewModel();

				return View(model);
			}

			return Unauthorized();
		}

		[HttpPost]
		[Authorize(Roles = $"{Administrator}, {BestUser}")]
		public async Task<IActionResult> Add(HeadphoneImportViewModel model, bool? rbIsWireless, bool? rbHasMicrophone)
		{
			if (rbIsWireless is null && rbHasMicrophone is null)
			{
				this.ModelState.AddModelError("IsWireless", ErrorMessageForUnselectedOption);
				this.ModelState.AddModelError("HasMicrophone", ErrorMessageForUnselectedOption);
			}
			else if (rbIsWireless is null)
			{
				this.ModelState.AddModelError("IsWireless", ErrorMessageForUnselectedOption);
				model.HasMicrophone = rbHasMicrophone;
			}
			else if (rbHasMicrophone is null)
			{
				this.ModelState.AddModelError("HasMicrophone", ErrorMessageForUnselectedOption);
				model.IsWireless = rbIsWireless;
			}
			else
			{
				model.IsWireless = rbIsWireless;
				model.HasMicrophone = rbHasMicrophone;
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
				int id = await this.headphoneService.AddHeadphoneAsync(model, userId);

				TempData[TempDataMessage] = ProductSuccessfullyAdded;

				return RedirectToAction(nameof(Details), new { id, information = model.GetInformation() });
			}
			catch (TechStoreException)
			{
				return View(ErrorCommonViewName);
			}
			catch (ArgumentException)
			{
				return View(ErrorCommonViewName);
			}
		}

		[HttpGet]
		[Authorize(Roles = $"{Administrator}, {BestUser}")]
		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				var headphone = await this.headphoneService.GetHeadphoneByIdAsHeadphoneEditViewModelAsync(id);

				if (this.User.IsInRole(BestUser)
				    && (headphone.Seller is null || this.User.Id() != headphone.Seller.UserId))
				{
					return Unauthorized();
				}

				return View(headphone);

			}
			catch (ArgumentException)
			{
				return NotFound();
			}
		}

		[HttpPost]
		[Authorize(Roles = $"{Administrator}, {BestUser}")]
		public async Task<IActionResult> Edit(HeadphoneEditViewModel model)
		{
			if (!this.ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var headphone = await this.headphoneService.GetHeadphoneByIdAsHeadphoneEditViewModelAsync(model.Id);

				if (this.User.IsInRole(BestUser)
				    && (headphone.Seller is null || this.User.Id() != headphone.Seller.UserId))
				{
					return Unauthorized();
				}

				int id = await this.headphoneService.EditHeadphoneAsync(model);

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
				var userHeadphones = await this.headphoneService.GetUserHeadphonesAsync(userId);

				return View(userHeadphones);
			}
			catch (TechStoreException)
			{
				return View(ErrorCommonViewName);
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
					var headphoneSeller = (await this.headphoneService.GetHeadphoneByIdAsHeadphoneEditViewModelAsync(id)).Seller;

					if (headphoneSeller is not null && headphoneSeller.UserId == userId)
					{
						return Unauthorized();
					}
				}

				ViewData["Title"] = "Buy a Headphone";

				await this.headphoneService.MarkHeadphoneAsBoughtAsync(id);

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
