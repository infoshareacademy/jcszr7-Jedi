using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using JediApp.Services.Helpers;

namespace JediApp.Services.Services
{
    public class MenuRoleUserService
    {
        private readonly IUserService _userService;
        private readonly IUserWalletRepository _userWalletRepository;
        private readonly IExchangeOfficeBoardService _exchangeOfficeBoardSevice;

        public MenuRoleUserService(IUserService userService, IUserWalletRepository userWalletRepository, IExchangeOfficeBoardService exchangeOfficeBoardSevice)
        {
            _userService = userService;
            _userWalletRepository = userWalletRepository;
            _exchangeOfficeBoardSevice = exchangeOfficeBoardSevice;
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

            _userWalletRepository.RegisterWalletToUser(user.Wallet.Id, user.Login, newcurrencycode, newcurrencyamount);
        }

        public Wallet GetWallet(User user)
        {
            var userWallet = _userWalletRepository.GetWallet(user.Wallet.Id);

            Console.WriteLine($"User: {user.Login} - your current balance:");
            foreach (var item in userWallet.WalletStatus)
            {
                Console.WriteLine($"{item.Currency}-{item.CurrencyAmount}");
            }

            return userWallet;
        }

        public void Deposit(User user)
        {
            var availableCurrencies = _exchangeOfficeBoardSevice.GetAllCurrencies();
            Console.WriteLine("Select available currency:");

            for (var i = 0; i < availableCurrencies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Currency: {availableCurrencies[i].Name}, Code: {availableCurrencies[i].ShortName}");
            }
            var selectedCurrencyIndex = MenuOptionsHelper.GetUserSelectionAndValidate(1, availableCurrencies.Count);
            var selectedCurrencyName = availableCurrencies[selectedCurrencyIndex - 1].ShortName;

            Console.WriteLine($"You selected: {selectedCurrencyName}");
            Console.WriteLine("Enter amout of money for deposit:");
            var deposit = MenuOptionsHelper.CheckDecimal(Console.ReadLine());

            _userWalletRepository.Deposit(user.Wallet.Id, user.Login, selectedCurrencyName, deposit);

            Console.WriteLine("Deposit successfull!\n");

            PrintWalletCurrencySaldo(user, selectedCurrencyName);
        }

        public void Withdrawal(User user)
        {
            var userWallet = _userWalletRepository.GetWallet(user.Wallet.Id);
            if (!userWallet.WalletStatus.Any())
            {
                Console.WriteLine("You don't have any currency in your deposit");
                return;
            }

            Console.WriteLine("Select available currency from you wallet:");
            for (var i = 0; i < userWallet.WalletStatus.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Currency: {userWallet.WalletStatus[i].Currency.ShortName}, current saldo: {userWallet.WalletStatus[i].CurrencyAmount}");
            }
            var selectedCurrencyIndex = MenuOptionsHelper.GetUserSelectionAndValidate(1, userWallet.WalletStatus.Count);
            var selectedCurrencyName = userWallet.WalletStatus[selectedCurrencyIndex - 1].Currency.ShortName;

            Console.WriteLine($"You selected: {selectedCurrencyName}");
            Console.WriteLine("Enter amout of money for withdrawal:");
            var withdrawal = MenuOptionsHelper.CheckDecimal(Console.ReadLine());

            _userWalletRepository.Withdrawal(user.Wallet.Id, user.Login, selectedCurrencyName, withdrawal);

            Console.WriteLine("Withdrawal successfull!\n");

            PrintWalletCurrencySaldo(user, selectedCurrencyName);
        }

        private void PrintWalletCurrencySaldo(User user, string selectedCurrencyName)
        {
            var userWallet = _userWalletRepository.GetWallet(user.Wallet.Id);
            var currentSaldo = userWallet.WalletStatus.Where(a => a.Currency.ShortName.Equals(selectedCurrencyName, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();

            Console.WriteLine($"Your current saldo of '{selectedCurrencyName}' is {currentSaldo.CurrencyAmount}");
        }
    }
}
