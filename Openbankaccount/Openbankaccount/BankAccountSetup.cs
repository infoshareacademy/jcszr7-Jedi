using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openbankaccount
{
    public class BankAccount
    {
        
        public string AccountNumber { get; }
        public Customer Client { get; }
        public Guid Id { get; } = Guid.NewGuid();

        public BankAccount(string accountNumber, Customer client)
        {
            
            if (accountNumber.Length != 20)
            {
                throw new Exception("Podany numer konta nie spelnia wymagan!");
            }
            this.AccountNumber = accountNumber;

            this.Client = client; 
            

        }
        public void print()
        {
            Console.WriteLine(this.AccountNumber);
            Console.WriteLine(this.Id);
            //Client.print(); 
        }
    }
}
