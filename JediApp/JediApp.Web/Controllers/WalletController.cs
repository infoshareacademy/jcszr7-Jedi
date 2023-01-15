using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class WalletController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
