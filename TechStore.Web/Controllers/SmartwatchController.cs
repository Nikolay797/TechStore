using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Smartwatch;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;

namespace TechStore.Web.Controllers
{
	[Authorize]
	public class SmartwatchController : Controller
	{
		private readonly ISmartwatchService smartwatchService;

		public SmartwatchController(ISmartwatchService smartwatchService)
		{
			this.smartwatchService = smartwatchService;
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
	}
}
