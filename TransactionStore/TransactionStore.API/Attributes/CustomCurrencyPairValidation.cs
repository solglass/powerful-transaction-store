using System;
using System.ComponentModel.DataAnnotations;
using TransactionStore.API.Utils;

namespace TransactionStore.API.Attributes
{
    public class CustomCurrencyPairValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Quotes.CurrencyPairs.ContainsKey((string)value);
        }
    }
}
