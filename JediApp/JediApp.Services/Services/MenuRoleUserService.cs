﻿using JediApp.Database.Domain;
using JediApp.Services.Helpers;

namespace JediApp.Services.Services
{
    public class MenuRoleUserService
    {
        private readonly IUserService _userService;

        public MenuRoleUserService(IUserService userService)
        {
            _userService = userService;
        }

        public void RegisterUser()
        {
            var user = new User();
            user.Role = UserRole.User;  //jako enum albo tabela w bazie

            Console.WriteLine("Enter new login");
            user.Login = MenuOptionsHelper.CheckString(Console.ReadLine());

            Console.WriteLine("Enter password");
            user.Password = MenuOptionsHelper.CheckString(Console.ReadLine());

            var registeredUser = _userService.AddUser(user);
            if (registeredUser != null)
            {
                Console.WriteLine($"Login {user.Login} has been created.");
            }
            else
            {
                if (!_userService.IfLoginIsUnique(user))
                {
                    Console.WriteLine($"Login {user.Login} has already been used. Try another one...");
                }
                else
                {
                    Console.WriteLine($"Login {user.Login} has NOT been created.");
                }
            }
        }
        public void RegisterWalletToUser(Guid userid)
        {
            var user = _userService.GetUserById(userid);
            Console.WriteLine("Enter the currency and its ammount");
            var newcurrencycode = MenuOptionsHelper.CheckString(Console.ReadLine());
            var newcurrencyamount = MenuOptionsHelper.CheckDecimal(Console.ReadLine());
            string fileName = "..//..//..//..//userwallets.csv";
            using (StreamWriter file = new StreamWriter(fileName, true))
            {
                file.WriteLine($"{user.Wallet.Id};{user.Login};{newcurrencycode};{newcurrencyamount}");
            }
        }

    }
}
