using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using JediApp.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JediApp.Web.Controllers
{
    public class ExchangeOfficeBoardController : Controller
    {
        private readonly IExchangeOfficeBoardService _exchangeOfficeBoardService;
        private readonly INbpJsonService _nbpJsonService;

        public ExchangeOfficeBoardController(IExchangeOfficeBoardService exchangeOfficeBoardService, INbpJsonService nbpJsonService)
        {
            _exchangeOfficeBoardService = exchangeOfficeBoardService;
            _nbpJsonService = nbpJsonService;
        }

        //public ExchangeOfficeBoardController()
        //{
        //    _exchangeOfficeBoardService = new ExchangeOfficeBoardService(new ExchangeOfficeBoardRepository());
        //}

        // GET: ExchangeOfficeBoardController
        public ActionResult Index()
        {
            ViewData["activePage"] = "AdminExchange";

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
            _exchangeOfficeBoardService.AddCurrency(newCurrency);

            return RedirectToAction(nameof(Index));

            if (!ModelState.IsValid)
            {
                return View(newCurrency);
            }
            try
            {
                _exchangeOfficeBoardService.AddCurrency(newCurrency);

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

        //// POST: ExchangeOfficeBoardController/Edit/5
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

            _exchangeOfficeBoardService.UpdateCurrency(id, currency);

            return RedirectToAction(nameof(Index));

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

        //// GET: ExchangeOfficeBoardController/Delete/5
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

        // POST: ExchangeOfficeBoardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCurrencyFromNbpApi()
        {

            List<Currency> nbpCurrencies = _nbpJsonService.GetAllCurrencies();

            foreach (var currency in nbpCurrencies)
            {
                _exchangeOfficeBoardService.AddCurrency(new Currency()
                {
                    Name = currency.Name,
                    ShortName = currency.ShortName,
                    Country = currency.Country,
                    BuyAt = currency.BuyAt,
                    SellAt = currency.SellAt
                });
            }

            return RedirectToAction(nameof(Index));

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Markup()
        {

            var currencies = _exchangeOfficeBoardService.GetAllCurrencies();

            foreach (var currency in currencies)
            {
                //spread 5%
                currency.BuyAt *= (decimal)1.025;
                currency.SellAt *= (decimal)0.975;
                _exchangeOfficeBoardService.UpdateCurrency(currency.Id, currency);
               
            }

            return RedirectToAction(nameof(Index));


        }
        
    }
}
