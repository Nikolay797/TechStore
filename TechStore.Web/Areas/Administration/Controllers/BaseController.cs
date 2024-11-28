using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]/{id?}")]
    [Authorize(Roles = Administrator)]
    public class BaseController : Controller
    {
    }
}
