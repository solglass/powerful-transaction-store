using System.Collections.Generic;
namespace TransactionStore.Core.Utils
{
    public class Quotes
    {
        public const string baseCurrency = "USD";
        public static Dictionary<string, decimal> CurrencyPair { get; set; }
    }
}
