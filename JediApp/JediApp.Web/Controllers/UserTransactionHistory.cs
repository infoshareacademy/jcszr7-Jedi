using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class UserTransactionHistory : Controller
    {
        public IActionResult Index()
        {
            ViewData["activePage"] = "UserTransactionHistory";

            return View();
        }
    }
}
