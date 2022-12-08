using JediApp.Database.Domain;
using Microsoft.AspNetCore.Mvc;

namespace JediApp.Database.Interfaces
{
    public interface IUserRepository
    {

        Task<User> GetUserById(string id);
        Task<IActionResult> GetAllUsers();//1
        //List<User> BrowseUsers(string query);
        //User AddUser(User user);
        //User GetLoginPassword(string login, string password);
    }
}
