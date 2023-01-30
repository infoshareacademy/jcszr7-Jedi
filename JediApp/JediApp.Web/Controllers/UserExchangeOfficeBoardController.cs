﻿using JediApp.Database.Domain;
using JediApp.Database.Interface;
using JediApp.Database.Repositories;
using JediApp.Services.Services;
using JediApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JediApp.Web.Controllers
{
    public class UserExchangeOfficeBoardController : Controller
    {
        private readonly IExchangeOfficeBoardService _exchangeOfficeBoardService;
        private readonly INbpJsonService _nbpJsonService;
        private readonly IExchangeOfficeService _exchangeOfficeService;
        private readonly IUserWalletService _userWalletService;


        public UserExchangeOfficeBoardController(IExchangeOfficeBoardService exchangeOfficeBoardService, INbpJsonService nbpJsonService, 
            IExchangeOfficeService exchangeOfficeService, IUserWalletService userWalletService)
        {
            _exchangeOfficeBoardService = exchangeOfficeBoardService;
            _nbpJsonService = nbpJsonService;
            _exchangeOfficeService = exchangeOfficeService;
            _userWalletService = userWalletService;
        }

        //public ExchangeOfficeBoardController()
        //{
        //    _exchangeOfficeBoardService = new ExchangeOfficeBoardService(new ExchangeOfficeBoardRepository());
        //}

        // GET: ExchangeOfficeBoardController
        public ActionResult Index()
        {
            ViewData["activePage"] = "UserExchangeOfficeBoard";

            var model = _exchangeOfficeBoardService.GetAllCurrencies();

            return View(model);
        }

        // GET: ExchangeOfficeBoardController/Details/5
        public ActionResult Details(Guid id)
        {
            var currency = _exchangeOfficeBoardService.GetCurrencyById(id);
            return View(currency);
        }

        // GET: ExchangeOfficeBoardController/Edit/5
        public ActionResult Buy(Guid id)
        {
            
            var currency = _exchangeOfficeBoardService.GetCurrencyById(id);
            var exchangeFromCurrency = _exchangeOfficeBoardService.GetAllCurrencies().Where(c => (c.ShortName.ToLower()).Equals("pln")).FirstOrDefault();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exchangeFromCurrencyAmount = _userWalletService.GetCurrencyBalanceById(userId, exchangeFromCurrency.Id).CurrencyAmount;

            if (currency != null)
            {
                var userExchange = new UserExchange()
                {
                    ExchangeFromCurrency = exchangeFromCurrency.ShortName,
                    ExchangeFromCurrencyAmount = exchangeFromCurrencyAmount,
                    ExchangeToCurrency = currency.ShortName,
                    ExchangeToCurrencyAmount = 0,
                    BuyAt = currency.BuyAt,
                    SellAt = currency.SellAt,
                    ExchangeMaxAmount = exchangeFromCurrencyAmount / currency.BuyAt

                };


                return View(userExchange);
                //return View(currency);
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
        public ActionResult Buy(UserExchange userExchange)
        {
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}

            //----
            //_exchangeOfficeBoardService.UpdateCurrency(id, currency);

            //return RedirectToAction(nameof(Index));

            //if (!ModelState.IsValid)
            //{
            //    return View(currency);
            //}

            //try
            //{
            //    _exchangeOfficeBoardService.UpdateCurrency(id, currency);
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
            //----
            //////

            if (userExchange.ExchangeToCurrencyAmount > userExchange.ExchangeMaxAmount)
            {
                ViewData["errorMessage"] = "Requested currency buy amount larger than current saldo.";
                ViewData["currentBalance"] = userExchange.ExchangeMaxAmount;
                return View("Buy", userExchange);
            }
            // Check balance after withdrawal

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _userWalletService.Withdrawal(userId, userExchange.ExchangeFromCurrency, userExchange.ExchangeToCurrencyAmount * userExchange.BuyAt, "Buy");
            _userWalletService.Deposit(userId, userExchange.ExchangeToCurrency, userExchange.ExchangeToCurrencyAmount, "Buy");

            //var newBalance = GetCurrencyBalance(userWithdrawal.Currency);
            var exchangeFromCurrency = _exchangeOfficeBoardService.GetAllCurrencies().Where(c => (c.ShortName.ToLower()).Equals(userExchange.ExchangeFromCurrency.ToLower())).FirstOrDefault();
            var newBalance = _userWalletService.GetCurrencyBalanceById(userId, exchangeFromCurrency.Id).CurrencyAmount;
            ViewData["currentSaldo"] = newBalance;
            ViewData["currenncy"] = userExchange.ExchangeFromCurrency;
            ViewData["activePage"] = "UserBuy";

            return View("BuyCompleted");

        }

        // GET: ExchangeOfficeBoardController/Edit/5
        public ActionResult Sell(Guid id)
        {

            //var currency = _exchangeOfficeBoardService.GetCurrencyById(id);
            var currency = _exchangeOfficeBoardService.GetAllCurrencies().Where(c => (c.ShortName.ToLower()).Equals("pln")).FirstOrDefault();
            var exchangeFromCurrency = _exchangeOfficeBoardService.GetCurrencyById(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var exchangeFromCurrencyAmount = _userWalletService.GetCurrencyBalanceById(userId, exchangeFromCurrency.Id).CurrencyAmount;
            var exchangeFromCurrencyAmount = _userWalletService.GetCurrencyBalanceById(userId, id).CurrencyAmount;

            if (currency != null)
            {
                var userExchange = new UserExchange()
                {
                    ExchangeFromCurrency = exchangeFromCurrency.ShortName,
                    ExchangeFromCurrencyAmount = exchangeFromCurrencyAmount,
                    ExchangeToCurrency = currency.ShortName,
                    ExchangeToCurrencyAmount = _userWalletService.GetCurrencyBalanceById(userId, currency.Id).CurrencyAmount,
                    BuyAt = currency.BuyAt,
                    SellAt = currency.SellAt,
                    ExchangeMaxAmount = exchangeFromCurrencyAmount

                };


                return View(userExchange);
                //return View(currency);
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
        public ActionResult Sell(UserExchange userExchange)
        {

            if (userExchange.ExchangeFromCurrencyAmount > userExchange.ExchangeMaxAmount)
            {
                ViewData["errorMessage"] = "Requested currency buy amount larger than current saldo.";
                ViewData["currentBalance"] = userExchange.ExchangeMaxAmount;
                return View("Sell", userExchange);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _userWalletService.Withdrawal(userId, userExchange.ExchangeFromCurrency, userExchange.ExchangeFromCurrencyAmount, "Sell");
            _userWalletService.Deposit(userId, userExchange.ExchangeToCurrency, userExchange.ExchangeFromCurrencyAmount * userExchange.SellAt, "Sell");

            var exchangetoCurrency = _exchangeOfficeBoardService.GetAllCurrencies().Where(c => (c.ShortName.ToLower()).Equals(userExchange.ExchangeToCurrency.ToLower())).FirstOrDefault();
            var newBalance = _userWalletService.GetCurrencyBalanceById(userId, exchangetoCurrency.Id).CurrencyAmount;
            ViewData["currentSaldo"] = newBalance;
            ViewData["currenncy"] = userExchange.ExchangeToCurrency;
            ViewData["activePage"] = "UserSell";

            return View("SellCompleted");

        }

    }
}
