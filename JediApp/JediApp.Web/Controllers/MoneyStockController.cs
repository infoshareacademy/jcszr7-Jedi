using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class MoneyStockController : Controller
    {
        public IActionResult Index()
        {
            ViewData["activePage"] = "MoneyStock";

            return View();
        }
    }
}
