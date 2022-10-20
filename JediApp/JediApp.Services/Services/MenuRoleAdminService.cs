using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using JediApp.Services.Helpers;

namespace JediApp.Services.Services
{
    public class MenuRoleAdminService
    {
        private readonly IUserService _userService;
        private readonly IExchangeOfficeBoardService _exchangeOfficeBoardSevice;

        private readonly INbpJsonService _nbpJsonService;

        public MenuRoleAdminService(IUserService userService, IExchangeOfficeBoardService exchangeOfficeBoardSevice, INbpJsonService nbpJsonService)
        {
            _userService = userService;
            _exchangeOfficeBoardSevice = exchangeOfficeBoardSevice;
            _nbpJsonService = nbpJsonService;

        private readonly IAvailableMoneyOnStockRepository _availableMoneyOnStock;

        public MenuRoleAdminService(IUserService userService, IExchangeOfficeBoardService exchangeOfficeBoardSevice, IAvailableMoneyOnStockRepository availableMoneyOnStock)
        {
            _userService = userService;
            _exchangeOfficeBoardSevice = exchangeOfficeBoardSevice;
            _availableMoneyOnStock = availableMoneyOnStock;

        }

        public void SearchByLogin()
        {
            Console.WriteLine("Enter login");
            var testLogin = MenuOptionsHelper.CheckString(Console.ReadLine());
            var testUserByLogin = _userService.BrowseUsers(testLogin);
            if (testUserByLogin != null)
            {
                foreach (var user in testUserByLogin)
                {
                    Console.WriteLine($"Id: {user.Id} Login: {user.Login} Password: {user.Password} Role: {user.Role}");
                }
                
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
                else
                {
                    Console.WriteLine($"{shortName} has not exist");
                }
            }
        }
        public void AddCurrencyFromNbpApi()

        {
            List<Currency> nbpCurrencies = _nbpJsonService.GetAllCurrencies();

            foreach (var currency in nbpCurrencies)
            {
                _exchangeOfficeBoardSevice.AddCurrency(new Currency()
                {
                    Name = currency.Name,
                    ShortName = currency.ShortName,
                    Country = currency.Country,
                    BuyAt = currency.BuyAt,
                    SellAt = currency.SellAt
                });
            }


        }

        public void AddMoneyToStock()
        {
            var availableCurrencies = _exchangeOfficeBoardSevice.GetAllCurrencies();
            Console.WriteLine("Select available currency:");

            for (var i = 0; i < availableCurrencies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Currency: {availableCurrencies[i].Name}, Country: {availableCurrencies[i].Country}");
            }
            var selectedCurrencyIndex = MenuOptionsHelper.GetUserSelectionAndValidate(1, availableCurrencies.Count);


            Console.WriteLine($"You selected {availableCurrencies[selectedCurrencyIndex - 1].Name}");
            Console.WriteLine("Enter amout of money:");
            var moneyInStock = MenuOptionsHelper.CheckDecimal(Console.ReadLine());

            var moneyToBeAdded = new MoneyOnStock();
            moneyToBeAdded.Value = moneyInStock;
            moneyToBeAdded.CurrencyName = availableCurrencies[selectedCurrencyIndex - 1].Name;

            _availableMoneyOnStock.AddMoneyToStock(moneyToBeAdded);
            Console.WriteLine($"{moneyInStock} {moneyToBeAdded.CurrencyName} has been added.");
        }

        public void ShowAvailableMoneyOnStock()
        {
            var moneyOnStock = _availableMoneyOnStock.GetAvailableMoneyOnStock();
            Console.WriteLine("Available money in exchange office:");
            foreach (MoneyOnStock item in moneyOnStock)
            {
                Console.WriteLine($"{item.Value} {item.CurrencyName}");
            }
        }
    }
}

