using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openbankaccount
{
    public class Customer
    {
        public string FirstName { get; private set; }
        public string LastName { get; }
        public string  Pesel { get; }

        public Customer(string firstName, string lastName, string pesel)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel p = new Pesel(pesel); 
            this.Pesel = p.get(); 
        }
        public void print() 
        {
            Console.WriteLine(this.FirstName);
            Console.WriteLine(this.LastName);
            Console.WriteLine(this.Pesel);
        }

    }
}
