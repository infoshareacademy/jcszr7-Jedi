using JediApp.Database.Domain;
using JediApp.Services.Helpers;

namespace JediApp.Services.Services
{
    public class MenuRoleUserService
    {
        private readonly string fileNameWallet = @"C:\Users\Albert\Desktop\jcszr7-Jedi\JediApp\userwallets.csv";

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

            using (StreamWriter file = new StreamWriter(fileNameWallet, true))
            {
                file.WriteLine($"{user.Wallet.Id};{user.Login};{newcurrencycode};{newcurrencyamount}");
            }
        }

        public void GetWallet(User user)
        {
            List<dynamic> list = new List<dynamic>();
            string logString = (user.Wallet.Id).ToString();

            var usersFromFile = File.ReadAllLines(fileNameWallet);

            foreach (var line in usersFromFile)
            {
                string[] columns = line.Split(";");

                list.Add(new { idUzytko = columns[0], waluta = columns[2], ilosc = decimal.Parse(columns[3]) });
            }

            var listItem = list.FirstOrDefault(x => x.idUzytko == logString);

            Console.WriteLine($"{listItem.waluta}-{listItem.ilosc} ");
        }

    }
}
