using JediApp.Database.Domain;
using JediApp.Database.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace JediApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        

        public UserService(IServiceProvider userRepository)
        {
            _userRepository = ActivatorUtilities.GetServiceOrCreateInstance<IUserRepository>(userRepository);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await  _userRepository.GetAllUsers();

            return users;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _userRepository.GetUserById(id);

            return  user;
        }

        public async Task<User> Delete(string id)
        {
            var user = await _userRepository.GetUserById(id);

            _userRepository.DeleteUser(user);

            return user;
        }

        //public void DeleteUser(string id)
        //{
        //    var user = _userRepository.GetUserById(id);
        //}
        //public User GetUserById(Guid id)
        //{
        //    return _userRepository.GetUserById(id);
        //}

        //public User GetUserByLogin(string login)
        //{
        //    return _userRepository.GetUserByLogin(login);
        //}

        //public List<User> GetAllUsers()
        //{

        //    return _userRepository.GetAllUsers();
        //}

        //public User AddUser(User user)
        //{
        //    if (IfLoginIsUnique(user))
        //    {
        //      return _userRepository.AddUser(user);
        //    }

        //    return null;
        //}

        //public List<User> BrowseUsers(string query)
        //{
        //    return _userRepository.BrowseUsers(query);
        //}

        //public bool IfLoginIsUnique(User user)
        //{
        //    foreach (var _user in GetAllUsers())
        //    {
        //        if(user.Login.Equals(_user.Login))
        //            return false;
        //    }
        //    return true;
        //}

    }
}
