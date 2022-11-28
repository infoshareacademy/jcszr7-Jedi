using JediApp.Database.Domain;
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
        public ActionResult Details(Guid id)
        {
            var currency = _exchangeOfficeBoardService.GetCurrencyById(id);
            return View(currency);
        }

        // GET: ExchangeOfficeBoardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExchangeOfficeBoardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Currency newCurrency)
        {
            if (!ModelState.IsValid)
            {
                return View(newCurrency);
            }
            try
            {
                newCurrency = _exchangeOfficeBoardService.AddCurrency(newCurrency);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExchangeOfficeBoardController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var currency = _exchangeOfficeBoardService.GetCurrencyById(id);
            if (currency != null)
            {
                return View(currency);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

            //return View();
        }

        // POST: ExchangeOfficeBoardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        public ActionResult Edit(Guid id, Currency currency)
        {
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}

            if (!ModelState.IsValid)
            {
                return View(currency);
            }

            try
            {
                _exchangeOfficeBoardService.UpdateCurrency(id, currency);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExchangeOfficeBoardController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var currency = _exchangeOfficeBoardService.GetCurrencyById(id);

            return View(currency);
        }

        // POST: ExchangeOfficeBoardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var currencyToDelete = _exchangeOfficeBoardService.GetCurrencyById(id);
                _exchangeOfficeBoardService.DeleteCurrencyByShortName(currencyToDelete.ShortName);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
