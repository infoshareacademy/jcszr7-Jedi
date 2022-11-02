namespace JediApp.Database.Domain
{
    public class User : Base
    {
        public string Login { get; set; }
        public string Password { get; set; }
        //public List<WalletPosition> WalletStatus { get; set; }
        public Wallet Wallet { get; set; }
        public UserRole Role { get; set; }
        public string printuser()
        {
            return $"{this.Id};{this.Login};{this.Password};{this.Role};{this.Wallet.Id};";
        }
        public string printuserwallet()
        {
            return $"{this.Id};{this.Login};{this.Wallet.Id};{this.Wallet.printwallet()}";
        }
        public User()
        {
            this.Wallet = new Wallet();
        }
    }


}
