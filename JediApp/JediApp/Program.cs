// See https://aka.ms/new-console-template for more information
using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using JediApp.Services.Services;

Console.WriteLine("**** JediApp ****");


//add app admin user
var adminUser = new User();
adminUser.Login = "admin";
adminUser.Password = "admin";
adminUser.Role = "admin";

IUserRepository userRepo = new UserRepository();
IUserService userService = new UserService(userRepo);
IMenuService menuService = new MenuService(userService);

userService.AddUser(adminUser);

//test menu
menuService.WelcomeMenu();