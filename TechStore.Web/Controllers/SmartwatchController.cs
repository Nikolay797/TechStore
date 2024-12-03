using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Exceptions;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Smartwatch;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;

namespace TechStore.Web.Controllers
{
	[Authorize]
	public class SmartwatchController : Controller
	{
		private readonly ISmartwatchService smartwatchService;
		private readonly IClientService clientService;

		public SmartwatchController(ISmartwatchService smartwatchService, IClientService clientService)
		{
			this.smartwatchService = smartwatchService;
			this.clientService = clientService;
		}

		[HttpGet]
		public async Task<IActionResult> Index([FromQuery] AllSmartwatchesQueryModel query)
		{
			var result = await this.smartwatchService.GetAllSmartwatchesAsync(
				query.Keyword,
				query.Sorting,
				query.CurrentPage);

			query.TotalProductsCount = result.TotalSmartwatchesCount;
			query.Smartwatches = result.Smartwatches;

			return View(query);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id, string information)
		{
			try
			{
				var smartwatch = await this.smartwatchService.GetSmartwatchByIdAsSmartwatchDetailsExportViewModelAsync(id);

				if (information.ToLower() != smartwatch.GetInformation().ToLower())
				{
					return NotFound();
				}

				return View(smartwatch);
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
				var smartwatch = await this.smartwatchService.GetSmartwatchByIdAsSmartwatchDetailsExportViewModelAsync(id);

				if (this.User.IsInRole(BestUser)
					&& (smartwatch.Seller is null || this.User.Id() != smartwatch.Seller.UserId))
				{
					return Unauthorized();
				}

				await this.smartwatchService.DeleteSmartwatchAsync(id);

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
						ViewData["Title"] = "Add a Smartwatch";

						return View(AddNotAllowedViewName);
					}
				}
				catch (TechStoreException)
				{
					return View(ErrorCommonViewName);
				}

				return View();
			}

			return Unauthorized();
		}

		[HttpPost]
		[Authorize(Roles = $"{Administrator}, {BestUser}")]
		public async Task<IActionResult> Add(SmartwatchImportViewModel model)
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
				int id = await this.smartwatchService.AddSmartwatchAsync(model, userId);

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
				var smartwatch = await this.smartwatchService.GetSmartwatchByIdAsSmartwatchEditViewModelAsync(id);

				if (this.User.IsInRole(BestUser)
				    && (smartwatch.Seller is null || this.User.Id() != smartwatch.Seller.UserId))
				{
					return Unauthorized();
				}

				return View(smartwatch);
			}
			catch (ArgumentException)
			{
				return NotFound();
			}
		}

		[HttpPost]
		[Authorize(Roles = $"{Administrator}, {BestUser}")]
		public async Task<IActionResult> Edit(SmartwatchEditViewModel model)
		{
			if (!this.ModelState.IsValid)
			{
				return View();
			}

			try
			{
				var smartwatch = await this.smartwatchService.GetSmartwatchByIdAsSmartwatchEditViewModelAsync(model.Id);

				if (this.User.IsInRole(BestUser)
				    && (smartwatch.Seller is null || this.User.Id() != smartwatch.Seller.UserId))
				{
					return Unauthorized();
				}

				int id = await this.smartwatchService.EditSmartwatchAsync(model);

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
				var userSmartwatches = await this.smartwatchService.GetUserSmartwatchesAsync(userId);

				return View(userSmartwatches);
			}
			catch (TechStoreException)
			{
				return View(ErrorCommonViewName);
			}
		}
	}
}
