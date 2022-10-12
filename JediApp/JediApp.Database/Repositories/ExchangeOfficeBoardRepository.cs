﻿using JediApp.Database.Domain;

namespace JediApp.Database.Repositories
{
    public class ExchangeOfficeBoardRepository : IExchangeOfficeBoardRepository
    {
        private readonly string fileName = "..//..//..//..//ExchangeOfficeBoard.csv"; //może przeniść do klasy statycznej ???
        public Currency AddCurrency(Currency currency)
        {
            var id = Guid.NewGuid();
            using (StreamWriter file = new StreamWriter(fileName, true))
            {
                file.WriteLine($"{id};{currency.Name};{currency.ShortName};{currency.Country};{currency.BuyAt};{currency.SellAt}");
            }

            return new Currency { Id = id, Name = currency.Name, ShortName = currency.ShortName, Country = currency.Country, BuyAt = currency.BuyAt, SellAt = currency.SellAt };
    
    }

        public List<Currency> GetAllCurrencies()
        {
            List<Currency> currencies = new();

            if (!File.Exists(fileName))
                return new List<Currency>();

            var currenciesFromFile = File.ReadAllLines(fileName);
            foreach (var line in currenciesFromFile)
            {
                var columns = line.Split(';');
                Guid.TryParse(columns[0], out var newGuid);
                decimal.TryParse(columns[4], out var buy);
                decimal.TryParse(columns[5], out var sell);
                currencies.Add(new Currency { Id = newGuid, Name = columns[1], ShortName = columns[2], Country = columns[3], BuyAt = buy, SellAt = sell});
            }
            return currencies;
        }

        public Currency GetCurrencyById(Guid id)
        {
            List<Currency> currencies = GetAllCurrencies();

            return currencies.SingleOrDefault(x => x.Id == id);
        }

        public List<Currency> BrowseCurrency(string query)
        {
            List<Currency> currencies = GetAllCurrencies();

            return currencies.Where(x => x.Name == query || x.ShortName == query || x.Country == query).ToList();
        }

    }
}