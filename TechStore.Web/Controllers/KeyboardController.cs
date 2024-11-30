using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Contracts;
using TechStore.Core.Extensions;
using TechStore.Core.Models.Keyboard;

namespace TechStore.Web.Controllers
{
    [Authorize]
    public class KeyboardController : Controller
    {
        private readonly IKeyboardService keyboardService;

        public KeyboardController(IKeyboardService keyboardService)
        {
            this.keyboardService = keyboardService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllKeyboardsQueryModel query)
        {
            var result = await this.keyboardService.GetAllKeyboardsAsync(query.Format, query.Type, query.Wireless,
                query.Keyword, query.Sorting, query.CurrentPage);

            query.TotalKeyboardsCount = result.TotalKeyboardsCount;

            query.Formats = await this.keyboardService.GetAllKeyboardsFormatsAsync();
            query.Types = await this.keyboardService.GetAllKeyboardsTypesAsync();

            query.Keyboards = result.Keyboards;

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string information)
        {
            try
            {
                var keyboard = await this.keyboardService.GetKeyboardByIdAsKeyboardDetailsExportViewModelAsync(id);

                if (information != keyboard.GetInformation())
                {
                    return NotFound();
                }

                return View(keyboard);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
