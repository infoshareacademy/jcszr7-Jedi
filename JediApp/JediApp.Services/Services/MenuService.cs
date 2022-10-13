using JediApp.Database.Domain;
using JediApp.Services.Helpers;

namespace JediApp.Services.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUserService _userService;
        private readonly MenuRoleUserService _menuUserActions;
        private readonly MenuRoleAdminService _menuAdminActions;
        private readonly IExchangeOfficeBoardService _exchangeOfficeBoardSevice;

        public MenuService(IUserService userService, IExchangeOfficeBoardService exchangeOfficeBoardSevice)
        {
            _userService = userService;
            _menuUserActions = new MenuRoleUserService(_userService);
            _menuAdminActions = new MenuRoleAdminService(_userService);
            _exchangeOfficeBoardSevice = exchangeOfficeBoardSevice;
        }

        public void WelcomeMenu()
        {
            Console.WriteLine("\nWelcome! Please select option:");
            Console.WriteLine("1. Login User");
            Console.WriteLine("2. Register User");
            Console.WriteLine("3. Exchange Office Board");
            Console.WriteLine("4. Exit");
            Console.WriteLine("You choose: ");

            int selectedOption = MenuOptionsHelper.GetUserSelectionAndValidate(1, 4);

            switch (selectedOption)
            {
                case 1:
                    // TODO: Add login ser

                    Console.WriteLine("Enter your login.");
                    string login = Console.ReadLine();
                    Console.WriteLine("Enter password");
                    string password = Console.ReadLine();

                    User user = new UserRepository().GetLoginPassword(login,password);

                    if (user != null)
                    {
                        if (user.Login == login)
                        {
                            Console.WriteLine("Password provided is correct");
                            Console.WriteLine($"Welcome {user.Login}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User password / login error.");
                    }

                    bool isUserAdmin = (user.Role=="admin");
                    
                    if (isUserAdmin)
                    {
                        AdminMenu();
                    }
                    else
                    {
                        UserMenu();
                    }
                    break;

                case 2:
                    _menuUserActions.RegisterUser();
                    WelcomeMenu();
                    break;
                case 3:
                    PrintExchangeOfficeBoard();
                    WelcomeMenu();
                    break;
                case 4: Console.WriteLine("Exit from app, bye, bye!!!");
                    Environment.Exit(0);
                    break;
                default: throw new Exception($"Option {selectedOption} not supported");
            };
        }

        

        public void AdminMenu()
        {
            Console.WriteLine("\nMenu: please select option:");
            Console.WriteLine("1. Search by login");
            Console.WriteLine("2. All user list");
            Console.WriteLine("3. Exit");
            Console.WriteLine("You choose: ");

            int selectedOption = MenuOptionsHelper.GetUserSelectionAndValidate(1, 3);

            switch (selectedOption)
            {
                case 1:
                    _menuAdminActions.SearchByLogin();
                    break;
                case 2:
                    _menuAdminActions.ListAllUsers();
                    break;
                case 3:
                    WelcomeMenu();
                    break;
                default: throw new Exception($"Option {selectedOption} not supported");
            };

            var toMainMenu = MenuOptionsHelper.GetBackToMainMenuQuestion();

            if (toMainMenu)
            {
                AdminMenu();
            }
            else
            {
                WelcomeMenu();
            }
        }

        public void UserMenu()
        {
            Console.WriteLine("\nMenu: please select option:");
            Console.WriteLine("1. Add new account number");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdrawal");
            Console.WriteLine("4. Exchange");
            Console.WriteLine("5. History");
            Console.WriteLine("6. Currency calculator");
            Console.WriteLine("7. Exit");
            Console.WriteLine("You choose: ");

            int selectedOption = MenuOptionsHelper.GetUserSelectionAndValidate(1, 7);

            switch (selectedOption)
            {
                case 1:
                    Console.WriteLine("Navigate to: Add new account number");
                    break;
                case 2:
                    Console.WriteLine("Navigate to: Deposit");
                    break;
                case 3:
                    Console.WriteLine("Navigate to: Withdrawal");
                    break;
                case 4:
                    Console.WriteLine("Navigate to: Exchange");
                    break;
                case 5:
                    Console.WriteLine("Navigate to: History");
                    break;
                case 6:
                    Console.WriteLine("Navigate to: Currency calculator");
                    break;
                case 7:
                    WelcomeMenu();
                    break;
                default: throw new Exception($"Option {selectedOption} not supported");
            };

            var toMainMenu = MenuOptionsHelper.GetBackToMainMenuQuestion();

            if (toMainMenu)
            {
                UserMenu();
            }
            else
            {
                WelcomeMenu();
            }
        }

        private void PrintExchangeOfficeBoard(string searchBy = null)
        {
            Console.Clear();
            List<Currency> exchangeOfficeBoard;
            if (searchBy is null)
            {
                exchangeOfficeBoard = _exchangeOfficeBoardSevice.GetAllCurrencies();
                Console.WriteLine("*=== Exchange Office Board ===*");

            }
            else
            {
                
                exchangeOfficeBoard = _exchangeOfficeBoardSevice.BrowseCurrency(searchBy);
                Console.WriteLine($"Result: {exchangeOfficeBoard.Count()}");
            }

            Console.WriteLine("| BuyAt | SellAt | ShortName | Name               | Country");
            for (int i = 0; i < exchangeOfficeBoard.Count(); i++)
            {
                WriteAt($"| {exchangeOfficeBoard[i].BuyAt}", 0, i + 2);
                WriteAt($"| {exchangeOfficeBoard[i].SellAt}", 8, i + 2);
                WriteAt($"| {exchangeOfficeBoard[i].ShortName}", 17, i + 2);
                WriteAt($"| {exchangeOfficeBoard[i].Name}", 29, i + 2);
                WriteAt($"| {exchangeOfficeBoard[i].Country}", 50, i + 2);
                Console.WriteLine();
            }
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Search for currency");
            Console.WriteLine("2. Show all curriences");
            Console.WriteLine("3. Exit");
            Console.WriteLine("You choose: ");

            int selectedOption = MenuOptionsHelper.GetUserSelectionAndValidate(1, 3);

            switch (selectedOption)
            {
                case 1:
                    Console.Write("searching... type name, shortname or country:");
                    searchBy = MenuOptionsHelper.CheckString(Console.ReadLine());
                    PrintExchangeOfficeBoard(searchBy);
                    break;
                case 2:
                    Console.Clear();
                    PrintExchangeOfficeBoard();
                    break;
                case 3:
                    Console.Clear();
                    WelcomeMenu();
                    break;
                default: throw new Exception($"Option {selectedOption} not supported");
            };
            
        }

        private void WriteAt(string s, int x, int y)
        {
            int origRow = 0;
            int origCol = 0;
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
