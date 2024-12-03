using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Headphone;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;

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
	}
}