using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class UserExchangeOfficeBoardController : Controller
    {
        public IActionResult Index()
        {
            ViewData["activePage"] = "UserExchange";

            return View();
        }
    }
}
