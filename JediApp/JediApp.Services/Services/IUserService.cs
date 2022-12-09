using JediApp.Database.Domain;

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
