using System;
using System.ComponentModel.DataAnnotations;
using TransactionStore.Business;

namespace TransactionStore.API.Attributes
{
    public class CustomCurrencyValidation : ValidationAttribute
    {
        private const int _currancyLength = 3;
        public override bool IsValid(object value)
        {
            string currancy = (string)value;
            return currancy.Length == _currancyLength
                && CurrencyRatesService.CurrencyPair.ContainsKey(CurrencyRatesService.baseCurrency + currancy);
        }
    }
}
