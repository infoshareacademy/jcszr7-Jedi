namespace JediApp.Database.Domain
{
    public class ExchangeOfficeBoard
    {
        public Guid Id { get; set; }
        public Guid ExchangeOfficeId { get; set; }

        public ExchangeOffice ExchangeOffice { get; set; }
        public ICollection<Currency> Currencies { get; set; }
    }
}
