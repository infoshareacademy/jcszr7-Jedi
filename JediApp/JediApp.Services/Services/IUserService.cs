using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JediApp.Services.Services
{
    public interface IUserService
    {
        User GetUserById(Guid id);
        User GetUserByLogin(string login);
        List<User> GetAllUsers();
        List<User> BrowseUsers(string query);
        User AddUser(User user);
        bool IfLoginIsUnique(User user);

    }
}
