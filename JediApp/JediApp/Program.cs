// See https://aka.ms/new-console-template for more information
using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using JediApp.Services.Services;
using System.Runtime.Intrinsics.X86;

Console.WriteLine("**** JediApp ****");


//add app admin user
var adminUser = new User();
adminUser.Login = "admin";
adminUser.Password = "admin";
adminUser.Role = UserRole.Admin;

var currencyPLN = new Currency() //add first currency PLN
{
    Name = "Złoty",
    ShortName = "PLN",
    Country = "Poland",
    BuyAt = 1,
    SellAt = 1
};

IUserRepository userRepo = new UserRepository();
IUserService userService = new UserService(userRepo);
IExchangeOfficeBoardRepository exchangeOfficeBoardRepo = new ExchangeOfficeBoardRepository();
IExchangeOfficeBoardService exchangeOfficeBoardSevice = new ExchangeOfficeBoardService(exchangeOfficeBoardRepo);
IMenuService menuService = new MenuService(userService, exchangeOfficeBoardSevice);

userService.AddUser(adminUser);
exchangeOfficeBoardSevice.AddCurrency(currencyPLN);


//test menu
menuService.WelcomeMenu();