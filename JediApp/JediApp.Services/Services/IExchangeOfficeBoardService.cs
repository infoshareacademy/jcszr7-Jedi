using JediApp.Database.Domain;

namespace JediApp.Services.Services
{
    public interface IExchangeOfficeBoardService
    {
        public Currency AddCurrency(Currency currency);
        List<Currency> GetAllCurrencies();
        Currency GetCurrencyById(Guid id);
        List<Currency> BrowseCurrency(string query);
    }
}