namespace JediApp.Database.Domain
{
    public class Currency : Base
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Country { get; set; }
        public decimal BuyAt { get; set; }
        public decimal SellAt { get; set; }

    }
}
