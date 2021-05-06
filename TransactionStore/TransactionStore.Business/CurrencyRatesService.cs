using System.Collections.Generic;
namespace TransactionStore.Business
{
    public class CurrencyRatesService
    {
        public string BaseCurrency = "USD";
        public Dictionary<string, decimal> CurrencyPair { get; set; }
    }
}
