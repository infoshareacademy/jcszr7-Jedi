using JediApp.Database.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JediApp.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string fileName = @"C...";
        public User AddUser (User user)
        {
            var id = Guid.NewGuid();
            using (StreamWriter file = new StreamWriter(fileName, true))
            {
                file.WriteLine($"{id};{user.Login};{user.Password};{user.Role}");
            }

            return new User{ Id = id, Login = user.Login, Password = user.Password, Role = user.Role };
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            if (!File.Exists(fileName))
                return new List<User>();

            var usersFromFile = File.ReadAllLines(fileName);
            foreach (var line in usersFromFile)
            {
                var columns = line.Split(';');
                Guid.TryParse(columns[0], out var newGuid);
                users.Add(new User { Id = newGuid, Login = columns[1], Password = columns[2], Role = columns[3] });
            }
            return users;
        }

        public User GetUserById(Guid id)
        {
            List<User> users = GetAllUsers();

            return users.SingleOrDefault(x => x.Id == id);
        }

        public User GetUserByLogin(string login)
        {
            List<User> users = GetAllUsers();

            return users.SingleOrDefault(x => x.Login == login);
        }

        public User GetLoginPassword(string login, string password)
        {
            List<User> users = new List<User>();

            var usersFromFile = File.ReadAllLines(fileName);

            foreach (var line in usersFromFile)
            {
                var columns = line.Split(';');
                if (columns.Length == 4)
                users.Add(new User { Login = columns[1], Password = columns[2], Role = columns[3]});
            }

            User user = users.FirstOrDefault(x => x.Login == login && x.Password == password );

            return user;
        }
    }
}
