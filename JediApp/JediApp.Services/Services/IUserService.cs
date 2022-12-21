using JediApp.Database.Domain;

namespace JediApp.Services.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(string id);
        //User GetUserByLogin(string login);
        Task<IEnumerable<User>> GetAllUsers();
        //List<User> BrowseUsers(string query);
        //void DeleteUser(string id);
        Task<User> Delete(string id);
        //bool IfLoginIsUnique(User user);

    }
}
