﻿namespace JediApp.Database.Domain
{
    public class MoneyOnStock : Base
    {
       public string CurrencyName { get; set; } 
       public decimal Value { get; set; }  
    }
}
