using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Models.Headphone;

namespace TechStore.Web.Controllers
{
	[Authorize]
	public class HeadphoneController : Controller
	{
		private readonly IHeadphoneService headphoneService;

		public HeadphoneController(IHeadphoneService headphoneService)
		{
			this.headphoneService = headphoneService;
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

			query.TotalHeadphonesCount = result.TotalHeadphonesCount;

			query.Types = await this.headphoneService.GetAllHeadphonesTypesAsync();

			query.Headphones = result.Headphones;

			return View(query);
		}
	}
}
