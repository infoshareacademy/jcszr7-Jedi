using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class AdminTransactionHistoryController : Controller
    {
        public IActionResult Index()
        {
            ViewData["activePage"] = "AdminTransactionHistory";

            return View();
        }
    }
}
