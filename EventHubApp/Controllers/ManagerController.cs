using Microsoft.AspNetCore.Mvc;

namespace EventHubApp.Web.Controllers
{
    public class ManagerController : BaseController
    {
        public IActionResult Index()
        {
            return this.Ok("I am manager!!!");
        }
    }
}
