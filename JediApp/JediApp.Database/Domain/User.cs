using IdentityModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;

namespace JediApp.Database.Domain
{
    public class User : IdentityUser

    {
        public string Login { get; set; }
        public string Password { get; set; }
        //public List<WalletPosition> WalletStatus { get; set; }
        public Wallet Wallet { get; set; }
        public UserRole Role { get; set; }
       
        public User()
        {
            this.Wallet = new Wallet();
        }
    }


}
