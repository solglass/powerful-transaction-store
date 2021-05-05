using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.Business
{
    public class ConverterService
    {
        public static Currency ConvertCurrencyStringToCurrencyEnum(string currency)
        {
            return (Currency)Enum.Parse(typeof(Currency), currency);
        }
        public static decimal ConvertAmount(string senderCurrency, string recipientCurrency, decimal amount)
        {
            CurrencyRatesService.CurrencyPair.TryGetValue(CurrencyRatesService.baseCurrency + senderCurrency, out decimal RecipientAccountAmount);
            CurrencyRatesService.CurrencyPair.TryGetValue(CurrencyRatesService.baseCurrency + recipientCurrency, out decimal SenderAccountAmount);
            return Decimal.Round((SenderAccountAmount / RecipientAccountAmount * amount), 4);
        }
    }
}
