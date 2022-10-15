using JediApp.Database.Domain;
using JediApp.Services.Helpers;

namespace JediApp.Services.Services
{
    public class MenuRoleAdminService
    {
        private readonly IUserService _userService;
        private readonly IExchangeOfficeBoardService _exchangeOfficeBoardSevice;

        public MenuRoleAdminService(IUserService userService, IExchangeOfficeBoardService exchangeOfficeBoardSevice)
        {
            _userService = userService;
            _exchangeOfficeBoardSevice = exchangeOfficeBoardSevice;
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

        public void AddCurrency()
        {
            Console.WriteLine("Add a new currency to the exchange office");
            Console.WriteLine("Enter Name");
            var name = MenuOptionsHelper.CheckString(Console.ReadLine());
            Console.WriteLine("Enter ShortName");
            var shortName = MenuOptionsHelper.CheckString(Console.ReadLine());
            Console.WriteLine("Enter Country");
            var country = MenuOptionsHelper.CheckString(Console.ReadLine());
            Console.WriteLine("Enter BuyAt");
            var buyAt = MenuOptionsHelper.CheckDecimal(Console.ReadLine());
            Console.WriteLine("Enter SellAt");
            var sellAt = MenuOptionsHelper.CheckDecimal(Console.ReadLine());

  
            _exchangeOfficeBoardSevice.AddCurrency(new Currency()
            {
                Name = name,
                ShortName = shortName,
                Country = country,
                BuyAt = buyAt,
                SellAt = sellAt
            });
        }

        public void DeleteCurrency()
        {
            Console.WriteLine("Enter shortname of currency you want to delete (x = abort):");
            var shortName = MenuOptionsHelper.CheckString(Console.ReadLine());
            if(!shortName.Equals("x"))
            {
                var status = _exchangeOfficeBoardSevice.DeleteCurrencyByShortName(shortName);
                if (status)
                {
                    Console.WriteLine($"{shortName} has been deleted");
                }
            }
        }


    }
}
