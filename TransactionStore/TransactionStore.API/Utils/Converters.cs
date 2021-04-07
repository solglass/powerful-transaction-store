using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.Core.Models;

namespace TransactionStore.API.Utils
{
    public class Converters
    {
        public static int CurrencyPairToCurrency(string inputCurrencyPair)
        {
            int currency = inputCurrencyPair switch
            {
                "RUB/RUB" => 1, "USD/RUB" => 1, "EUR/RUB" => 1, "JPY/RUB" => 1,
                "USD/USD" => 2, "RUB/USD" => 2, "EUR/USD" => 2, "JPY/USD" => 2,
                "EUR/EUR" => 3, "RUB/EUR" => 3, "USD/EUR" => 3, "JPY/EUR" => 3,
                "JPY/JPY" => 4, "RUB/JPY" => 4, "USD/JPY" => 4, "EUR/JPY" => 4,
                _ => 0
            };
            return currency;
        }
        public static decimal ConvertAmount(string inputCurrencyPair, decimal amount)
        {
            if (Quotes.CurrencyPairs.TryGetValue(inputCurrencyPair, out decimal value))
            {
                return amount * value;
            }
            return amount;
        }
    }
}
