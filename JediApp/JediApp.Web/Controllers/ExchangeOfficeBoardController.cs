using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using JediApp.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class ExchangeOfficeBoardController : Controller
    {
        IExchangeOfficeBoardService _exchangeOfficeBoardService;

        public ExchangeOfficeBoardController(IExchangeOfficeBoardService exchangeOfficeBoardService)
        {
            _exchangeOfficeBoardService = exchangeOfficeBoardService;
        }

        //public ExchangeOfficeBoardController()
        //{
        //    _exchangeOfficeBoardService = new ExchangeOfficeBoardService(new ExchangeOfficeBoardRepository());
        //}

        // GET: ExchangeOfficeBoardController
        public ActionResult Index()
        {
            var model = _exchangeOfficeBoardService.GetAllCurrencies();

            return View(model);
        }

        // GET: ExchangeOfficeBoardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExchangeOfficeBoardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExchangeOfficeBoardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExchangeOfficeBoardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExchangeOfficeBoardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExchangeOfficeBoardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExchangeOfficeBoardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
