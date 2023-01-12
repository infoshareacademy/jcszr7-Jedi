using JediApp.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class AdminTransactionHistoryController : Controller
    {
        private readonly ITransactionHistoryService _transactionHistoryService;

        public AdminTransactionHistoryController(ITransactionHistoryService transactionHistoryService)
        {
            _transactionHistoryService = transactionHistoryService;
        }
        public IActionResult Index()
        {
            ViewData["activePage"] = "AdminTransactionHistory";

            var model = _transactionHistoryService.GetAllUsersHistories();

            return View(model);

        }
    }
}
