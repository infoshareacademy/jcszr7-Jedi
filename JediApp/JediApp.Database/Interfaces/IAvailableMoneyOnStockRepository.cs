using JediApp.Database.Domain;

namespace JediApp.Database.Interfaces
{
    public interface IAvailableMoneyOnStockRepository
    {
        void AddMoneyToStock(MoneyOnStock moneyOnStock);
        List<MoneyOnStock> GetAvailableMoneyOnStock();
    }

}
