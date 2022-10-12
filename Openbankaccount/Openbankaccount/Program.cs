
using Openbankaccount;

var firstName = Console.ReadLine();
var lastName = Console.ReadLine();
var pesel = Console.ReadLine();
var accountNumber = Console.ReadLine();

Customer customer1 = new Customer(firstName, lastName, pesel); 
customer1.print();

BankAccount account1 = new BankAccount(accountNumber, customer1);
account1.print();

Console.ReadKey();