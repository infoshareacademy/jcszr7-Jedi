using Microsoft.AspNetCore.Identity;

namespace JediApp.Database.Domain
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


}
