using JediApp.Services.Helpers;

namespace JediApp.Services.Services
{
    public class MenuRoleAdminService
    {
        private readonly IUserService _userService;

        public MenuRoleAdminService(IUserService userService)
        {
            _userService = userService;
        }

        public void SearchByLogin()
        {
            Console.WriteLine("Enter login");
            var testLogin = MenuOptionsHelper.CheckString(Console.ReadLine());
            var testUserByLogin = _userService.GetUserByLogin(testLogin);
            if (testUserByLogin != null)
            {
                Console.WriteLine($"Id: {testUserByLogin.Id} Login: {testUserByLogin.Login} Password: {testUserByLogin.Password} Role: {testUserByLogin.Role}");
            }
            else
            {
                Console.WriteLine($"Login {testLogin} has not been found ");
            }
        }

        public void ListAllUsers()
        {
            Console.WriteLine("List of users:");
            foreach (var item in _userService.GetAllUsers())
            {
                Console.WriteLine($"Id: {item.Id} Login: {item.Login} Password: {item.Password}  Role: {item.Role}");
            }
        }
    }
}
