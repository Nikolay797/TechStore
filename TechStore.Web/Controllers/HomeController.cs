using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (this.User.IsInRole(Administrator))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "Administration" });
            }

            return View();
        }

        [Route("/StatusCodeError/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("Error404");
            }
            else if (statusCode == 401 || statusCode == 403)
            {
                return View("Error401");
            }
            
            return View("Error500");
        }
    }
}
