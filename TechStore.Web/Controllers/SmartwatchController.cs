using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Models.Smartwatch;

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
	}
}
