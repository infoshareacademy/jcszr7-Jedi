using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JediApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(Guid id)
        {
            return _userRepository.GetUserById(id);
        }

        public User GetUserByLogin(string login)
        {
            return _userRepository.GetUserByLogin(login);
        }

        public List<User> GetAllUsers()
        {

            return _userRepository.GetAllUsers();
        }

        public void AddUser(User user)
        {
            if (IfLoginIsUnique(user))
            {
                _userRepository.AddUser(user);
            }

        }

        public bool IfLoginIsUnique(User user)
        {
            foreach (var _user in GetAllUsers())
            {
                if(user.Login.Equals(_user.Login))
                    return false;
            }
            return true;
        }
    }
}
