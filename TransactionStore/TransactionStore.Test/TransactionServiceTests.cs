using NUnit.Framework;

using TransactionStore.Core.Enums;
using System.Collections.Generic;
using TransactionStore.Business;
using TransactionStore.Data;
using Microsoft.Extensions.Options;
using TransactionStore.Core.Settings;

namespace TransactionStore.Test
{
    public class TransactionServiceTests
    {
        //private ITransactionService _transactionService;
        //private ITransactionRepository _transactionRepository;
        //private CurrencyRatesService _currencyRatesService;
        //private ConverterService _converterService;
        //[SetUp]
        //public void SetUp()
        //{
        //    _transactionRepository = new TransactionRepository(IOptions<AppSettings> options);
        //    _transactionService = new TransactionService();
        //    _converterService = new ConverterService(_currencyRatesService);
        //    _currencyRatesService.CurrencyPair = new Dictionary<string, decimal>()
        //    {
        //        ["USDUSD"] = 1,
        //        ["USDRUB"] = (decimal)0.01333333333333,
        //        ["USDEUR"] = (decimal)1.2,
        //        ["USDJPY"] = (decimal)0.009
        //    };
        //}
        //[TestCase("RUB", 1)]
        //[TestCase("USD", 2)]
        //[TestCase("EUR", 3)]
        //[TestCase("JPY", 4)]
        //public void ConvertCurrencyStringToCurrencyEnumPositiveTest(string inputCurrency, Currency expected)
        //{
        //    var actual = _converterService.ConvertCurrencyStringToCurrencyEnum(inputCurrency);

        //    Assert.AreEqual(expected, actual);
        //}
        //[TestCase("RUB", "USD", 1000000, 75000000)]
        //[TestCase("JPY", "EUR", 2000, 266666.6667)]
        //[TestCase("USD", "JPY", 0, 0)]
        //public void ConvertAmountPositiveTest(string senderCurrency, string recipientCurrency, decimal amount, decimal expected)
        //{
        //    var actual = _converterService.ConvertAmount(senderCurrency, recipientCurrency, amount);

        //    Assert.AreEqual(expected, actual);
        //}
        //[TestCase("USRUB")]
        //[TestCase("USL")]
        //[TestCase("12@")]
        //[TestCase(null)]
        //public void ConvertCurrencyStringToCurrencyEnumNegativeTest(string inputCurrency)
        //{
        //    try
        //    {
        //        _converterService.ConvertCurrencyStringToCurrencyEnum(inputCurrency);
        //    }
        //    catch
        //    {
        //        Assert.Pass();
        //    }
        //    Assert.Fail();
        //}
        //[TestCase("USD","EURJ", 2000)]
        //[TestCase("12@#13#@$*23","EUR", 1000000)]
        //[TestCase(null, "JPY", 1000000)]
        //[TestCase("RUB", null, 0)]
        //public void ConvertAmountNegativeTest(string senderCurrency, string recipientCurrency, decimal amount)
        //{
        //    try
        //    {
        //        _converterService.ConvertAmount(senderCurrency, recipientCurrency, amount);
        //    }
        //    catch
        //    {
        //        Assert.Pass();
        //    }
        //    Assert.Fail();
        //}
    }
}