using JediApp.Database.Domain;
using JediApp.Database.Interface;
using JediApp.Web.Areas.Identity.Data;

namespace JediApp.Database.Repositories
{
    public class ExchangeOfficeBoardRepositoryDB : IExchangeOfficeBoardRepository
    {
        private readonly JediAppDbContext _jediAppDb;

        public ExchangeOfficeBoardRepositoryDB(JediAppDbContext jediAppDb)
        {
            _jediAppDb = jediAppDb;
        }

        public Currency AddCurrency(Currency currency)
        {
            currency.ExchangeOfficeBoardId = _jediAppDb.ExchangeOfficeBoards.FirstOrDefault().Id; //tymczasowo M

            //_jediAppDb.AddAsync(currency);
            //_jediAppDb.SaveChangesAsync();

            _jediAppDb.Add(currency);
            _jediAppDb.SaveChanges();

            return currency;

        }

        public Currency UpdateCurrency(Currency currency)
        {
            var currencyFromDb = _jediAppDb.Currencys.Where(c => c.ShortName == currency.ShortName).FirstOrDefault();
            currencyFromDb.SellAt = currency.SellAt;
            currencyFromDb.BuyAt = currency.BuyAt;

            _jediAppDb.Update(currencyFromDb);
            _jediAppDb.SaveChanges();

            return currencyFromDb;
        }

        public List<Currency> GetAllCurrencies()
        {
            //tmp check if officeBoard exist
            var office = _jediAppDb.ExchangeOffices.FirstOrDefault();
            if (office is null)
            {
                office = new ExchangeOffice
                {
                    Id = Guid.NewGuid(),
                    Name = "Jedi Web Exchange Office",
                    Address = "ul. Wiejska 4/6/8 00-902 Warszawa",
                    Markup = 5
                };

                _jediAppDb.Add(office);
                _jediAppDb.SaveChanges();
            }

            var officeBoard = _jediAppDb.ExchangeOfficeBoards.FirstOrDefault();
            if(officeBoard is null)
            {
               
                officeBoard = new ExchangeOfficeBoard { Id = Guid.NewGuid(), ExchangeOfficeId = office.Id };
                _jediAppDb.Add(officeBoard);
                _jediAppDb.SaveChanges();

            }
            //end tmp
            //tmp check if currencies exist
            var pln = _jediAppDb.Currencys.Where(c => (c.ShortName.ToLower()).Equals("pln")).FirstOrDefault();
            if (pln is null)
            {
                pln = new Currency
                {
                    Id = Guid.NewGuid(),
                    Name = "Polish zloty",
                    ShortName = "PLN",
                    Country = "Poland",
                    BuyAt = 1,
                    SellAt = 1,
                    ExchangeOfficeBoardId = _jediAppDb.ExchangeOfficeBoards.FirstOrDefault().Id
                };
                _jediAppDb.Add(pln);
            }

            var euro = _jediAppDb.Currencys.Where(c => (c.ShortName.ToLower()).Equals("eur")).FirstOrDefault();
            if (euro is null)
            {
                euro = new Currency
                {
                    Id = Guid.NewGuid(),
                    Name = "Euro",
                    ShortName = "EUR",
                    Country = "European Union (EU)",
                    BuyAt = 1,
                    SellAt = 1,
                    ExchangeOfficeBoardId = _jediAppDb.ExchangeOfficeBoards.FirstOrDefault().Id
                };
                _jediAppDb.Add(euro);
            }

            var usd = _jediAppDb.Currencys.Where(c => (c.ShortName.ToLower()).Equals("usd")).FirstOrDefault();
            if (usd is null)
            {
                usd = new Currency
                {
                    Id = Guid.NewGuid(),
                    Name = "United States dollar",
                    ShortName = "USD",
                    Country = "United States",
                    BuyAt = 1,
                    SellAt = 1,
                    ExchangeOfficeBoardId = _jediAppDb.ExchangeOfficeBoards.FirstOrDefault().Id
                };
                _jediAppDb.Add(usd);
            }

            var chf = _jediAppDb.Currencys.Where(c => (c.ShortName.ToLower()).Equals("chf")).FirstOrDefault();
            if (chf is null)
            {
                chf = new Currency
                {
                    Id = Guid.NewGuid(),
                    Name = "Swiss franc",
                    ShortName = "CHF",
                    Country = "Switzerland",
                    BuyAt = 1,
                    SellAt = 1,
                    ExchangeOfficeBoardId = _jediAppDb.ExchangeOfficeBoards.FirstOrDefault().Id
                };
                _jediAppDb.Add(chf);
            }

            var gbp = _jediAppDb.Currencys.Where(c => (c.ShortName.ToLower()).Equals("gbp")).FirstOrDefault();
            if (gbp is null)
            {
                gbp = new Currency
                {
                    Id = Guid.NewGuid(),
                    Name = "British pound",
                    ShortName = "GBP",
                    Country = "United Kingdom",
                    BuyAt = 1,
                    SellAt = 1,
                    ExchangeOfficeBoardId = _jediAppDb.ExchangeOfficeBoards.FirstOrDefault().Id
                };
                _jediAppDb.Add(gbp);
            }

            //end tmp
            _jediAppDb.SaveChanges();

            return _jediAppDb.Currencys.OrderBy(c => c.ShortName).ToList();

        }

        public Currency GetCurrencyById(Guid id)
        {
            List<Currency> currencies = GetAllCurrencies();

            return currencies.SingleOrDefault(x => x.Id == id);
        }

        public List<Currency> BrowseCurrency(string query)
        {
            List<Currency> currencies = GetAllCurrencies();

            return currencies.Where(x => x.Name.ToLowerInvariant().Contains(query.ToLowerInvariant())
                                      || x.ShortName.ToLowerInvariant().Contains(query.ToLowerInvariant())
                                      || x.Country.ToLowerInvariant().Contains(query.ToLowerInvariant())).ToList();
        }

        public bool UpdateCurrency(Guid id, Currency currencyToEdit)
        {

            try
            {
                var currency = GetCurrencyById(id);

                currency.Name = currencyToEdit.Name;
                currency.ShortName = currencyToEdit.ShortName;
                currency.Country = currencyToEdit.Country;
                currency.BuyAt = currencyToEdit.BuyAt;
                currency.SellAt = currencyToEdit.SellAt;

                //_jediAppDb.SaveChangesAsync();
                _jediAppDb.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteCurrency(Guid id)
        {

            try
            {
                var currency = GetCurrencyById(id);
                _jediAppDb.Remove(currency);
                //_jediAppDb.SaveChangesAsync();
                _jediAppDb.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}