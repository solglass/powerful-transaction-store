using System.Collections.Generic;
namespace TransactionStore.Business
{
    public class CurrencyRatesService
    {
        public const string baseCurrency = "USD";
        public static Dictionary<string, decimal> CurrencyPair { get; set; }
    }
}
