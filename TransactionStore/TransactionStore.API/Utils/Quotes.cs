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
                    ["RUB/RUB"] = 1, ["USD/RUB"] = 75,               ["EUR/RUB"] = 100,                 ["JPY/RUB"] = (decimal)0.7 ,
                    ["USD/USD"] = 1, ["RUB/USD"] = (decimal)0.01333, ["EUR/USD"] = (decimal)1.33333,    ["JPY/USD"] = (decimal)0.00933,
                    ["EUR/EUR"] = 1, ["RUB/EUR"] = (decimal)0.01,    ["USD/EUR"] = (decimal)0.75,       ["JPY/EUR"] = (decimal)0.007,
                    ["JPY/JPY"] = 1, ["RUB/JPY"] = (decimal)1.42857, ["USD/JPY"] = (decimal)107.14286,  ["EUR/JPY"] = (decimal)142.85714
                };
            }
        }
    }
}
