using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Mice;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;

namespace TechStore.Web.Controllers
{
	public class MouseController : Controller
	{
		private readonly IMouseService mouseService;

		public MouseController(IMouseService mouseService)
		{
			this.mouseService = mouseService;
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
	}
}
