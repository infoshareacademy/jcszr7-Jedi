using JediApp.Database.Domain;

namespace JediApp.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(Guid id);
        User GetUserByLogin(string login);
        void GetAllUsers();
        List<User> BrowseUsers(string query);
        User AddUser(User user);
        bool IfLoginIsUnique(User user);

    }
}
