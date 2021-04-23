using System;
using System.ComponentModel.DataAnnotations;
using TransactionStore.Core.Utils;

namespace TransactionStore.API.Attributes
{
    public class CustomCurrencyPairValidation : ValidationAttribute
    {
        private const int _currancyPairLength = 6;
        public override bool IsValid(object value)
        {
            string currancyPair = (string)value;
            return currancyPair.Length == _currancyPairLength
                && Quotes.CurrencyPair.ContainsKey(currancyPair.Substring(0, 3) + Quotes.baseCurrency) 
                && Quotes.CurrencyPair.ContainsKey(currancyPair.Substring(3, 3) + Quotes.baseCurrency);
        }
    }
}
