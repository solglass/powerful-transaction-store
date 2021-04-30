using System.Collections.Generic;
namespace TransactionStore.Core.Utils
{
    public class Quotes
    {
        public const string baseCurrency = "USD";
        public static Dictionary<string, decimal> CurrencyPair { get; set; } = new  Dictionary<string, decimal>
        {
            ["USDUSD"] = 1,
            ["USDRUB"] = (decimal)0.01333333333333,
            ["USDEUR"] = (decimal)1.2,
            ["USDJPY"] = (decimal)0.009,
        };
    }
}
