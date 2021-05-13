using System;
using System.Collections.Generic;
using System.Data;
using TransactionStore.Core.Enums;
using TransactionStore.Core.CustomExceptions;
using System.Threading.Tasks;

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

        public async Task<Currency> ConvertCurrencyStringToCurrencyEnumAsync(string currency)
        {
            return await Task.Run(() => ConvertCurrencyStringToCurrencyEnum(currency));
        }
        public decimal ConvertAmount(string senderCurrency, string recipientCurrency, decimal amount)
        {
            if (senderCurrency == recipientCurrency) return Decimal.Round(amount, 4);
            if (!IsValid(senderCurrency) || !IsValid(recipientCurrency)) throw new Exception("Currency is not valid");
            _currencyRatesService.CurrencyPair.TryGetValue(_currencyRatesService.BaseCurrency + senderCurrency, out decimal RecipientAccountAmount);
            _currencyRatesService.CurrencyPair.TryGetValue(_currencyRatesService.BaseCurrency + recipientCurrency, out decimal SenderAccountAmount);
            return Decimal.Round((SenderAccountAmount / RecipientAccountAmount * amount), 4);
        }

        public async Task<decimal> ConvertAmountAsync(string senderCurrency, string recipientCurrency, decimal amount)
        {
            return await Task.Run(() => ConvertAmount(senderCurrency, recipientCurrency, amount));
        }

        public DataTable ConvertListToDataTable(List<int> data)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));

            foreach (var id in data)
                table.Rows.Add(id);
            return table;
        }

        public async Task<DataTable> ConvertListToDataTableAsync(List<int> data)
        {
            return await Task.Run(() => ConvertListToDataTable(data));
        }

        private bool IsValid(string currency)
        {
            if (_currencyRatesService.CurrencyPair == null) throw new CurrencyRatesServiceException();
            return currency.Length == _currencyLength
                && _currencyRatesService.CurrencyPair.ContainsKey(_currencyRatesService.BaseCurrency + currency);
        }
    }
}
