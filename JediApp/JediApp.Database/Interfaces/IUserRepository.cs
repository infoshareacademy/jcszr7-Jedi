using JediApp.Database.Domain;

namespace JediApp.Database.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(Guid id);
        List<User> GetAllUsers();
        User AddUser(User user);
        
    }
}
