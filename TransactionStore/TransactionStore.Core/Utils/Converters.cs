using TransactionStore.Core.Enums;
using System;

namespace TransactionStore.Core.Utils
{
    public class Converters
    {
        public static Currency ConvertCurrencyStringToCurrencyEnum(string currency)
        {
            return (Currency)Enum.Parse(typeof(Currency), currency);
        }
        public static decimal ConvertAmount(string senderCurrency, string recipientCurrency, decimal amount)
        {
            Quotes.CurrencyPair.TryGetValue(Quotes.baseCurrency + senderCurrency, out decimal RecipientAccountAmount);
            Quotes.CurrencyPair.TryGetValue(Quotes.baseCurrency + recipientCurrency, out decimal SenderAccountAmount);
            return Decimal.Round((SenderAccountAmount / RecipientAccountAmount * amount), 4);
        }
    }
}
