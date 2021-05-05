using System;
using System.ComponentModel.DataAnnotations;
using TransactionStore.Business;

namespace TransactionStore.API.Attributes
{
    public class CustomCurrencyPairValidation : ValidationAttribute
    {
        private const int _currancyPairLength = 6;
        public override bool IsValid(object value)
        {
            string currancyPair = (string)value;
            return currancyPair.Length == _currancyPairLength
                && CurrencyRatesService.CurrencyPair.ContainsKey(CurrencyRatesService.baseCurrency + currancyPair.Substring(0, 3)) 
                && CurrencyRatesService.CurrencyPair.ContainsKey(CurrencyRatesService.baseCurrency + currancyPair.Substring(3, 3));
        }
    }
}
