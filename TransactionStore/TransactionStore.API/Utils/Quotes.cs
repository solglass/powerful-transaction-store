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
                    ["RUBRUB"] = 1, ["USDRUB"] = 75,               ["EURRUB"] = 100,                 ["JPYRUB"] = (decimal)0.7 ,
                    ["USDUSD"] = 1, ["RUBUSD"] = (decimal)0.01333, ["EURUSD"] = (decimal)1.33333,    ["JPYUSD"] = (decimal)0.00933,
                    ["EUREUR"] = 1, ["RUBEUR"] = (decimal)0.01,    ["USDEUR"] = (decimal)0.75,       ["JPYEUR"] = (decimal)0.007,
                    ["JPYJPY"] = 1, ["RUBJPY"] = (decimal)1.42857, ["USDJPY"] = (decimal)107.14286,  ["EURJPY"] = (decimal)142.85714
                };
            }
        }
        
    }
}
