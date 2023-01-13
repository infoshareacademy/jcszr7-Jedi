using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class UserDepositController : Controller
    {
        public IActionResult Index()
        {
            ViewData["activePage"] = "UserDeposit";
            return View();
        }
    }
}
