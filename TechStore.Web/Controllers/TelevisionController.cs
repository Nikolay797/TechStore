using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TechStore.Core.Contracts;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Television;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Web.Controllers
{
    [Authorize]
    public class TelevisionController : Controller
    {
        private readonly ITelevisionService televisionService;

        public TelevisionController(ITelevisionService televisionService)
        {
            this.televisionService = televisionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllTelevisionsQueryModel query)
        {
            var result = await this.televisionService.GetAllTelevisionsAsync(
                query.Brand,
                query.DisplaySize,
                query.Resolution,
                query.Keyword,
                query.Sorting,
                query.CurrentPage);

            query.TotalTelevisionsCount = result.TotalTelevisionsCount;

            query.Brands = await this.televisionService.GetAllBrandsNames();
            query.DisplaySizes = await this.televisionService.GetAllDisplaysSizesValues();
            query.Resolutions = await this.televisionService.GetAllResolutionsValues();

            query.Televisions = result.Televisions;

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var television = await this.televisionService.GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync(id);

                return View(television);
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
		        var television = await this.televisionService.GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync(id);
				
		        if (this.User.IsInRole(BestUser) && (television.Seller is null || this.User.Id() != television.Seller.UserId))
		        {
			        return Unauthorized();
				}

		        await this.televisionService.DeleteTelevisionAsync(id);

		        return RedirectToAction(nameof(Index));
			}
	        catch (ArgumentException)
	        {
				return NotFound();
			}
        }
	}
}
