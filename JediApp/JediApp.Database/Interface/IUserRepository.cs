using JediApp.Database.Domain;

namespace JediApp.Database.Interface
{
    public interface IUserRepository
    {
        //User GetUserById(Guid id);
        //User GetUserByLogin(string login);
        Task<IEnumerable<User>> GetAllUsers();
        //List<User> BrowseUsers(string query);
        //User AddUser(User user);
        //User GetLoginPassword(string login, string password);
    }
}
