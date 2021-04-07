using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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
