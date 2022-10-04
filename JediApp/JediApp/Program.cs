// See https://aka.ms/new-console-template for more information
using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using JediApp.Services.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("**** JediApp ****");


//add app admin user
var adminUser = new User();
adminUser.Login = "admin";
adminUser.Password = "admin";
adminUser.Role = "admin";

IUserRepository userRepo = new UserRepository();
IUserService userService = new UserService(userRepo);

userService.AddUser(adminUser);

//test menu
while (true)
{
    Console.WriteLine("select option: u - register user, s - search by login, l - all user list, x - exit");
    var  choice = checkString(Console.ReadLine());
    if (string.Equals(choice, "u"))
    {
        var user = new User();
        user.Role = "user";

        Console.WriteLine("Podaj login");
        user.Login = checkString(Console.ReadLine());

        Console.WriteLine("Podaj hasło");
        user.Password = checkString(Console.ReadLine());

        userService.AddUser(user);

    }
    else if(string.Equals(choice, "s")){
        //Console.WriteLine("test get admin by login");
        Console.WriteLine("Podaj login");
        var testLogin = checkString(Console.ReadLine());
        var testUserByLogin = userService.GetUserByLogin(testLogin);
        Console.WriteLine($"Id: {testUserByLogin.Id} Login: {testUserByLogin.Login} Password: {testUserByLogin.Password}");

        //Console.WriteLine("test get admin by id");
        //var testUserById = userService.GetUserById(new Guid("7fae4d64-a1b0-4fb2-91b9-a7dc7d30b71f"));  //your Guid in user.csv can be generated different, copy/paste from your file 
        //Console.WriteLine($"Id: {testUserById.Id} Login: {testUserById.Login} Password: {testUserById.Password}");
    }
    else if (string.Equals(choice, "l"))
    {
        Console.WriteLine("list of users:");
        foreach (var item in userService.GetAllUsers())
        {
            Console.WriteLine($"Id: {item.Id} Login: {item.Login} Password: {item.Password}");
        }
    }
    else if (string.Equals(choice, "x"))
    {
        break;
    }

}






string checkString(string input)
{
    while (true)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Brak danych! Podaj ponownie... ");
            input = Console.ReadLine();
        }
        else
        {
            return input;
        }
    }

}