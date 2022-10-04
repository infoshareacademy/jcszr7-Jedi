using JediApp.Database.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JediApp.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string fileName = "..//..//..//..//users.csv";
        public void AddUser (User user)
        {
            using (StreamWriter file = new StreamWriter(fileName, true))
            {
                file.WriteLine($"{Guid.NewGuid()};{user.Login};{user.Password}");
            }
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
                users.Add(new User { Id = newGuid, Login = columns[1], Password = columns[2] });
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
    }
}
