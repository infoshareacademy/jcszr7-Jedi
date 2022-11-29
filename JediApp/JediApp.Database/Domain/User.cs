using IdentityModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;

namespace JediApp.Database.Domain
{
    public class User : IdentityUser

    {
        public  string Name { get; set; }
        public string SurrName { get; set; }
    }


}
