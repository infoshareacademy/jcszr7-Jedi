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
adminUser.Role = "admin";

IUserRepository userRepo = new UserRepository();
IUserService userService = new UserService(userRepo);

userService.AddUser(adminUser);

//test menu
while (true)
{
    Console.WriteLine("\nMenu: please select option: u - register user, s - search by login, l - all user list, x - exit");
    var  choice = checkString(Console.ReadLine());
    
    if (string.Equals(choice, "u")) // register user
    {
        var user = new User();
        user.Role = "user";

        Console.WriteLine("Enter new login");
        user.Login = checkString(Console.ReadLine());

        Console.WriteLine("Enter password");
        user.Password = checkString(Console.ReadLine());

        var registeredUser = userService.AddUser(user);
        if (registeredUser != null)
        {
            Console.WriteLine($"Login {user.Login} has been created.");
        }
        else
        {
            if (!userService.IfLoginIsUnique(user))
            {
                Console.WriteLine($"Login {user.Login} has already been used. Try another one...");
            }
            else
            {
                Console.WriteLine($"Login {user.Login} has NOT been created.");
            }
            
        }

    }
    else if(string.Equals(choice, "s")){ //search by login
        //Console.WriteLine("test get by login");
        Console.WriteLine("Enter login");
        var testLogin = checkString(Console.ReadLine());
        var testUserByLogin = userService.GetUserByLogin(testLogin);
        if(testUserByLogin != null)
        {
            Console.WriteLine($"Id: {testUserByLogin.Id} Login: {testUserByLogin.Login} Password: {testUserByLogin.Password} Role: {testUserByLogin.Role}");
        }
        else
        {
            Console.WriteLine($"Login {testLogin} has not been found ");
        }

        //Console.WriteLine("test get by id");
        //var testUserById = userService.GetUserById(new Guid("7fae4d64-a1b0-4fb2-91b9-a7dc7d30b71f"));  //your Guid in user.csv can be generated different, copy/paste from your file 
        //Console.WriteLine($"Id: {testUserByLogin.Id} Login: {testUserByLogin.Login} Password: {testUserByLogin.Password} Role: {testUserByLogin.Role}");
    }
    else if (string.Equals(choice, "l")) //all user list
    {
        Console.WriteLine("List of users:");
        foreach (var item in userService.GetAllUsers())
        {
            Console.WriteLine($"Id: {item.Id} Login: {item.Login} Password: {item.Password}  Role: {item.Role}");
        }
    }
    else if (string.Equals(choice, "x")) //exit
    {
        Console.WriteLine("Exit from app, bye, bye !!");
        break;
    }

}

//Console.WriteLine("Enter your login.");
//string login = Console.ReadLine();
//Console.WriteLine("Enter password");
//string password = Console.ReadLine();

//var userCheck = userRepo.GetLoginPassword(login, password); 

//while (true)
//{
//    if (userCheck != null)
//    {
//        if (userCheck.Login == login)
//        {
//            Console.WriteLine("Password provided is correct");
//            Console.WriteLine($"Welcome {userCheck.Login}");
//            //menu method user
//        }
//        break;
//    }
//    else
//    {
//        Console.WriteLine("User password / login error.");
//        Thread.Sleep(3000);
//        break;
//        //back to main menu
//    }

//}




string checkString(string input)
{
    while (true)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("No input! Please type something... ");
            input = Console.ReadLine();
        }
        else
        {
            return input;
        }
    }

}