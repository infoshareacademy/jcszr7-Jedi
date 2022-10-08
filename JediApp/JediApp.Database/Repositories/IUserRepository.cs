using JediApp.Database.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JediApp.Database.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(Guid id);
        User GetUserByLogin(string login);
        List<User> GetAllUsers();
        User AddUser(User user);
    }
}
