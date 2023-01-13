using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class UserWithdrawalController : Controller
    {
        public IActionResult Index()
        {
            ViewData["activePage"] = "UserWithdrawal";
            return View();
        }
    }
}
