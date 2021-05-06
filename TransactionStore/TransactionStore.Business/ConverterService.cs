using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.Business
{
    public class ConverterService
    {
        private int _currencyLength = 3;
        private CurrencyRatesService _currencyRatesService;
        public ConverterService(CurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }
        public Currency ConvertCurrencyStringToCurrencyEnum(string currency)
        {
            if (!IsValid(currency)) throw new Exception("Currency is not valid");
            return (Currency)Enum.Parse(typeof(Currency), currency);
        }
        public decimal ConvertAmount(string senderCurrency, string recipientCurrency, decimal amount)
        {
            if (!IsValid(senderCurrency) || !IsValid(recipientCurrency)) throw new Exception("Currency is not valid");
            _currencyRatesService.CurrencyPair.TryGetValue(_currencyRatesService.BaseCurrency + senderCurrency, out decimal RecipientAccountAmount);
            _currencyRatesService.CurrencyPair.TryGetValue(_currencyRatesService.BaseCurrency + recipientCurrency, out decimal SenderAccountAmount);
            return Decimal.Round((SenderAccountAmount / RecipientAccountAmount * amount), 4);
        }
        private bool IsValid(string currency)
        {
            return currency.Length == _currencyLength
                && _currencyRatesService.CurrencyPair.ContainsKey(_currencyRatesService.BaseCurrency + currency);
        }
    }
}
