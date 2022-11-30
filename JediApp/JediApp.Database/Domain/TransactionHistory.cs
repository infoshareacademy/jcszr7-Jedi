namespace JediApp.Database.Domain
{
    public class TransactionHistory : Base
    {
        public Guid UserId { get; set; }
        public string UserLogin { get; set; }
        public string CurrencyName { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public string Description { get; set; }

    }
}
