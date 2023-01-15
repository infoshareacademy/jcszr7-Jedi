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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> UpdateUser(User user)
        {
            var users = await _userRepository.GetUserById(user.Id);

            users.FirstName = user.FirstName;
            users.LastName = user.LastName;
            users.UserName = user.UserName;
            users.Email = user.Email;

            return users;
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
