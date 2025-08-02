using Microsoft.AspNetCore.Mvc;

namespace EventHubApp.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
