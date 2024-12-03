using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Headphone;

namespace TechStore.Web.Controllers
{
	[Authorize]
	public class HeadphoneController : Controller
	{
		private readonly IHeadphoneService _headphoneService;

		public HeadphoneController(IHeadphoneService headphoneService)
		{
			_headphoneService = headphoneService;
		}

		[HttpGet]
		public async Task<IActionResult> Index([FromQuery] AllHeadphonesQueryModel query)
		{
			var result = await _headphoneService.GetAllHeadphonesAsync(
				query.Type,
				query.Wireless,
				query.Keyword,
				query.Sorting,
				query.CurrentPage);

			query.TotalHeadphonesCount = result.TotalHeadphonesCount;
			query.Types = await _headphoneService.GetAllHeadphonesTypesAsync();
			query.Headphones = result.Headphones;

			return View(query);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id, string information)
		{
			try
			{
				var headphone = await _headphoneService.GetHeadphoneByIdAsHeadphoneDetailsExportViewModelAsync(id);

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
	}
}