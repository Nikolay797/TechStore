using Microsoft.AspNetCore.Mvc;

namespace TechStore.Web.Areas.Administration.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
