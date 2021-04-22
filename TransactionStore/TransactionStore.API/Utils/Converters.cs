using TransactionStore.Core.Enums;
using System;

namespace TransactionStore.API.Utils
{
    public class Converters
    {
       public static Currency ConvertSenderCurrencyPairToCurrency(string inputCurrencyPair)
        {
            return (Currency)Enum.Parse(typeof(Currency), inputCurrencyPair.Substring(0, 3));
        }
        public static Currency ConvertRecipientCurrencyPairToCurrency(string inputCurrencyPair)
        {
            return (Currency)Enum.Parse(typeof(Currency), inputCurrencyPair.Substring(3, 3));
        }
        public static decimal ConvertAmount(string inputCurrencyPair, decimal amount)
        {
            Quotes.CurrencyPair.TryGetValue(inputCurrencyPair.Substring(0, 3) + Quotes.baseCurrency, out decimal senderAmount);
            Quotes.CurrencyPair.TryGetValue(inputCurrencyPair.Substring(3, 3) + Quotes.baseCurrency, out decimal recipientAmount);
            return Decimal.Round((senderAmount / recipientAmount * amount), 4);
        }
    }
}
