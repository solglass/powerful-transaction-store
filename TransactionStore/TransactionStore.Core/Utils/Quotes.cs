﻿using System.Collections.Generic;
namespace TransactionStore.Core.Utils
{
    public class Quotes
    {
        public const string baseCurrency = "USD";
        public static string field;
        public static Dictionary<string, decimal> CurrencyPair
        {
            get
            {
                return new Dictionary<string, decimal>
                {
                    ["USDUSD"] = 1, 
                    ["RUBUSD"] = (decimal)0.01333333333333,               
                    ["EURUSD"] = (decimal)1.2,                 
                    ["JPYUSD"] = (decimal)0.009 ,
                };
            }
        }
        
    }
}
