﻿using JediApp.Database.Domain;

namespace JediApp.Database.Repositories
{
    public interface IExchangeOfficeBoardRepository
    {
        Currency AddCurrency(Currency currency);
        List<Currency> GetAllCurrencies();
        Currency GetCurrencyById(Guid id);
        List<Currency> BrowseCurrency(string query);
        bool DeleteCurrency(Guid id);
    }
}