using JediApp.Database.Domain;

namespace JediApp.Database.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id);
        //User GetUserByLogin(string login);
        Task<IEnumerable<User>> GetAllUsers();
        void DeleteUser(User user);
        //List<User> BrowseUsers(string query);
        //User AddUser(User user);
        //User GetLoginPassword(string login, string password);
    }
}
