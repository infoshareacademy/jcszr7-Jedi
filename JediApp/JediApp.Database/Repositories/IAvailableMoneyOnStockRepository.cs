using JediApp.Database.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JediApp.Database.Repositories
{
    public interface IAvailableMoneyOnStockRepository
    {
        void AddMoneyToStock(MoneyOnStock moneyOnStock);
        List<MoneyOnStock> GetAvailableMoneyOnStock();
    }
    
}
