using JediApp.Services.Helpers;

namespace JediApp.Services.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUserService _userService;
        private readonly MenuRoleUserService _menuUserActions;
        private readonly MenuRoleAdminService _menuAdminActions;

        public MenuService(IUserService userService)
        {
            _userService = userService;
            _menuUserActions = new MenuRoleUserService(_userService);
            _menuAdminActions = new MenuRoleAdminService(_userService);
        }

        public void WelcomeMenu()
        {
            Console.WriteLine("\nWelcome! Please select option:");
            Console.WriteLine("1. Login User");
            Console.WriteLine("2. Register User");
            Console.WriteLine("3. Exit");
            Console.WriteLine("You choose: ");

            int selectedOption = MenuOptionsHelper.GetUserSelectionAndValidate(1, 3);

            switch (selectedOption)
            {
                case 1:
                    // TODO: Add login ser
                    bool isUserAdmin = false; //Hardcoded for now. Should be determined during Login
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
                case 3: Console.WriteLine("Exit from app, bye, bye!!!");
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
    }
}
