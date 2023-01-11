using JediApp.Database.Domain;
using JediApp.Database.Interface;
using JediApp.Web.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

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