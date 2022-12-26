using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JediApp.Database.Domain
{
    public class ExchangeOffice
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ExchangeOfficeBoardId { get; set; }

        public ExchangeOfficeBoard ExchangeOfficeBoard { get; set; }
        public User User { get; set; }
        public ICollection<MoneyOnStock> MoneyOnStocks { get; set; }
    }
}
