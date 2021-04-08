using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Utils
{
    public class Quotes
    {
        public static Dictionary<string, decimal> CurrencyPairs
        {
            get
            {
                return new Dictionary<string, decimal>
                {
                    ["USDRUB"] = 75,               
                    ["EURRUB"] = 100,                 
                    ["JPYRUB"] = (decimal)0.7
                };
            }
        }
    }
}
