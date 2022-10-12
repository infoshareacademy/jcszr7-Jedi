using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openbankaccount
{
    public class Pesel
    {
        private string _pesel;
        public Pesel (string pesel)
        {
            if (pesel.Length != 11)
            {
                throw new Exception("Niepoprawna dlugosc znakow");
            }
            if (pesel[3] != '2')
            {
                throw new Exception("Niepoprawny znak");
            }
            _pesel = pesel;
        }
        public string get()
        {
            return _pesel;
        }
    }
            
}
