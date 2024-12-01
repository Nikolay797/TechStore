using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Exceptions;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Mice;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;

namespace TechStore.Web.Controllers
{
	public class MouseController : Controller
	{
		private readonly IMouseService mouseService;
		private readonly IClientService clientService;

		public MouseController(IMouseService mouseService, IClientService clientService)
		{
			this.mouseService = mouseService;
			this.clientService = clientService;
		}

		[HttpGet]
		public async Task<IActionResult> Index([FromQuery] AllMiceQueryModel query)
		{
			var result = await this.mouseService.GetAllMiceAsync(
				query.Type,
				query.Sensitivity,
				query.Wireless,
				query.Keyword,
				query.Sorting,
				query.CurrentPage);

			query.TotalMiceCount = result.TotalMiceCount;

			query.Types = await this.mouseService.GetAllMiceTypesAsync();
			query.Sensitivities = await this.mouseService.GetAllMiceSensitivitiesAsync();

			query.Mice = result.Mice;

			return View(query);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id, string information)
		{
			try
			{
				var mouse = await this.mouseService.GetMouseByIdAsMouseDetailsExportViewModelAsync(id);

				if (information.ToLower() != mouse.GetInformation().ToLower())
				{
					return NotFound();
				}

				return View(mouse);

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
				var mouse = await this.mouseService.GetMouseByIdAsMouseDetailsExportViewModelAsync(id);

				if (this.User.IsInRole(BestUser)
				    && (mouse.Seller is null || this.User.Id() != mouse.Seller.UserId))
				{
					return Unauthorized();
				}

				await this.mouseService.DeleteMouseAsync(id);

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
						ViewData["Title"] = "Add a Mouse";

						return View(AddNotAllowedViewName);
					}
				}
				catch (TechStoreException)
				{
					return View(ErrorCommonViewName);
				}
			}

			var model = new MouseImportViewModel
			{
				Sensitivities = await this.mouseService.GetAllMiceSensitivitiesAsync()
			};

			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = $"{Administrator}, {BestUser}")]
		public async Task<IActionResult> Add(MouseImportViewModel model, bool? rbIsWireless, string? rbSensitivity)
		{
			if (rbIsWireless is null && rbSensitivity is null)
			{
				this.ModelState.AddModelError("IsWireless", ErrorMessageForUnselectedOption);

				this.ModelState.AddModelError("Sensitivity", ErrorMessageForUnselectedOption);
			}
			else if (rbIsWireless is null)
			{
				this.ModelState.AddModelError("IsWireless", ErrorMessageForUnselectedOption);

				model.Sensitivity = rbSensitivity;
			}
			else if (rbSensitivity is null)
			{
				this.ModelState.AddModelError("Sensitivity", ErrorMessageForUnselectedOption);

				model.IsWireless = rbIsWireless;
			}
			else
			{
				model.IsWireless = rbIsWireless;

				model.Sensitivity = rbSensitivity;
			}

			if (!this.ModelState.IsValid)
			{
				model.Sensitivities = await this.mouseService.GetAllMiceSensitivitiesAsync();

				return View(model);
			}

			string? userId = null;

			if (this.User.IsInRole(BestUser))
			{
				userId = this.User.Id();
			}

			try
			{
				int id = await this.mouseService.AddMouseAsync(model, userId);

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
	}
}
