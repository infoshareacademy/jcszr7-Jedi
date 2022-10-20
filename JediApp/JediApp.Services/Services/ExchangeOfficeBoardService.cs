using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JediApp.Services.Services
{
    public class ExchangeOfficeBoardService : IExchangeOfficeBoardService
    {
        private readonly IExchangeOfficeBoardRepository _exchangeOfficeBoardRepository;

        public ExchangeOfficeBoardService(IExchangeOfficeBoardRepository exchangeOfficeBoardRepository)
        {
            _exchangeOfficeBoardRepository = exchangeOfficeBoardRepository;
        }
        public Currency AddCurrency(Currency currency)
        {
            if (IfShortNameIsUnique(currency)) //because ShortName is unique
            {
                return _exchangeOfficeBoardRepository.AddCurrency(currency);
            }
            return null;
        }

        public List<Currency> GetAllCurrencies()
        {
            return _exchangeOfficeBoardRepository.GetAllCurrencies();
        }

        public Currency GetCurrencyById(Guid id)
        {
            return _exchangeOfficeBoardRepository.GetCurrencyById(id);
        }

        public List<Currency> BrowseCurrency(string query)
        {
            return _exchangeOfficeBoardRepository.BrowseCurrency(query);
        }

        public bool DeleteCurrencyByShortName(string shortName)
        {
            var currnecy = GetAllCurrencies().FirstOrDefault(c => c.ShortName == shortName);
            return _exchangeOfficeBoardRepository.DeleteCurrency(currnecy.Id);
        }

        public bool IfShortNameIsUnique(Currency currency)
        {
            foreach (var _currency in GetAllCurrencies())
            {
                if (currency.ShortName.Equals(_currency.ShortName))
                    return false;
            }
            return true;
        }



    }
}
