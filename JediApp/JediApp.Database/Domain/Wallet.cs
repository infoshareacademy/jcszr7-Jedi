namespace JediApp.Database.Domain
{
    public class Wallet 
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        
        public User User { get; set; }
        public ICollection<WalletPosition> WalletPositions { get; set; }
        
    }
   
}
