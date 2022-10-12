using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JediApp.Services.Services
{
    public interface IMenuService
    {
        public void WelcomeMenu();
        public void AdminMenu();
        public void UserMenu();
    }
}
