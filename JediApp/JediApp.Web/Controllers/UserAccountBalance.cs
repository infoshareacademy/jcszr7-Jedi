using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class UserAccountBalance : Controller
    {
        public IActionResult Index()
        {
            ViewData["activePage"] = "AccountBalance";

            return View();
        }
    }
}
