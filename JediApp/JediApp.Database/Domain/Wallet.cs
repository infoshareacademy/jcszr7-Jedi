using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JediApp.Database.Domain
{
    public class Wallet : Base
    {
        public Guid UserId { get; set; }
        public List<WalletPosition> WalletStatus { get; set; }
        public string printwallet()
        {
            var result = "";
            foreach (var position in WalletStatus)
            {
                result += $"{position.Currency.Name};{position.Currency.ShortName};{position.CurrencyAmount};{position.WalletId}";
            }
            return (result);
        }

        public Wallet()
        {
            WalletStatus = new List<WalletPosition>();
            Id = Guid.NewGuid();
        }
        public Wallet(string walletid)
        {
            WalletStatus = new List<WalletPosition>();
            Id = Guid.Parse(walletid);
        }

    }

    public class WalletPosition : Base
    {
        public decimal CurrencyAmount { get; set; }
        public Currency Currency { get; set; }
        public Guid WalletId { get; set; }
        public string UserName { get; set; }
        public WalletPosition()
        {
            WalletId = Guid.NewGuid();
        }
        public WalletPosition(string walletId)
        {
            WalletId = Guid.Parse(walletId);
        }
    }
}
