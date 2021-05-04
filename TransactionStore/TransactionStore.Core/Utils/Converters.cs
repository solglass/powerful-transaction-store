using TransactionStore.Core.Enums;
using System;

namespace TransactionStore.Core.Utils
{
    public class Converters
    {
       public static Currency ConvertRecipientAccountCurrencyPairToCurrency(string inputCurrencyPair)
        {
            return (Currency)Enum.Parse(typeof(Currency), inputCurrencyPair.Substring(3, 3));
        }
        public static Currency ConvertSenderAccountCurrencyPairToCurrency(string inputCurrencyPair)
        {
            return (Currency)Enum.Parse(typeof(Currency), inputCurrencyPair.Substring(0, 3));
        }
        public static decimal ConvertAmount(string inputCurrencyPair, decimal amount)
        {
            Quotes.CurrencyPair.TryGetValue(Quotes.baseCurrency + inputCurrencyPair.Substring(0, 3), out decimal RecipientAccountAmount);
            Quotes.CurrencyPair.TryGetValue(Quotes.baseCurrency + inputCurrencyPair.Substring(3, 3), out decimal SenderAccountAmount);
            return Decimal.Round((SenderAccountAmount / RecipientAccountAmount * amount), 4);
        }
    }
}
