using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.Core.Models;

namespace TransactionStore.API.Utils
{
    public class Converters
    {
       public static int SenderCurrencyPairToCurrency(string inputCurrencyPair)
        {
            int currency = inputCurrencyPair switch
            {
                "RUBRUB" => 1, "USDRUB" => 2, "EURRUB" => 3, "JPYRUB" => 4,
                "USDUSD" => 2, "RUBUSD" => 1, "EURUSD" => 3, "JPYUSD" => 4,
                "EUREUR" => 3, "RUBEUR" => 1, "USDEUR" => 2, "JPYEUR" => 4,
                "JPYJPY" => 4, "RUBJPY" => 1, "USDJPY" => 2, "EURJPY" => 3,
                _ => 0
            };
            return currency;
        }
        public static int RecipientCurrencyPairToCurrency(string inputCurrencyPair)
        {
            int currency = inputCurrencyPair switch
            {
                "RUBRUB" => 1, "USDRUB" => 1, "EURRUB" => 1, "JPYRUB" => 1,
                "USDUSD" => 2, "RUBUSD" => 2, "EURUSD" => 2, "JPYUSD" => 2,
                "EUREUR" => 3, "RUBEUR" => 3, "USDEUR" => 3, "JPYEUR" => 3,
                "JPYJPY" => 4, "RUBJPY" => 4, "USDJPY" => 4, "EURJPY" => 4,
                _ => 0
            };
            return currency;
        }
        public static decimal ConvertAmount(string inputCurrencyPair, decimal amount)
        {
            Quotes.CurrencyPairs.TryGetValue(inputCurrencyPair, out decimal value);
            return amount * value;
        }
    }
}
