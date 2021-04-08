using System;
using System.ComponentModel.DataAnnotations;
using TransactionStore.API.Utils;

namespace TransactionStore.API.Attributes
{
    public class CustomCurrencyPairValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string currancyPair = (string)value;
            return Quotes.Currency.ContainsKey(currancyPair.Substring(0, 3)) && Quotes.Currency.ContainsKey(currancyPair.Substring(3, 3));
        }
    }
}
