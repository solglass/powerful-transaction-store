using EducationSystem.Core.Enums;
using System;

namespace TransactionStore.API.Utils
{
    public class Converters
    {
       public static Currency SenderCurrencyPairToCurrency(string inputCurrencyPair)
        {
            return (Currency)Enum.Parse(typeof(Currency), inputCurrencyPair.Substring(0, 3));
        }
        public static Currency RecipientCurrencyPairToCurrency(string inputCurrencyPair)
        {
            return (Currency)Enum.Parse(typeof(Currency), inputCurrencyPair.Substring(3, 3));
        }
        public static decimal ConvertAmount(string inputCurrencyPair, decimal amount)
        {
            Quotes.Currency.TryGetValue(inputCurrencyPair.Substring(0, 3), out decimal senderAmount);
            Quotes.Currency.TryGetValue(inputCurrencyPair.Substring(3, 3), out decimal recipientAmount);
            return senderAmount / recipientAmount * amount;
        }
    }
}
