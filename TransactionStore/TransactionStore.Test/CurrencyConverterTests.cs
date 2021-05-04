using NUnit.Framework;
using TransactionStore.Core.Utils;
using TransactionStore.Core.Enums;
using System.Collections.Generic;

namespace TransactionStore.Test
{
    public class CurrencyConverterTests
    {
        [SetUp]
        public void SetUp()
        {
            Quotes.CurrencyPair = new Dictionary<string, decimal>()
            {
                ["USDUSD"] = 1,
                ["USDRUB"] = (decimal)0.01333333333333,
                ["USDEUR"] = (decimal)1.2,
                ["USDJPY"] = (decimal)0.009
            };
        }
        [TestCase("RUBUSD", 1)]
        [TestCase("USDRUB", 2)]
        [TestCase("EURRUB", 3)]
        [TestCase("JPYEUR", 4)]
        public void ConvertSenderAccountCurrencyPairToCurrencyPositiveTest(string inputCurrencyPair, Currency expected)
        {
            var actual = Converters.ConvertSenderAccountCurrencyPairToCurrency(inputCurrencyPair);

            Assert.AreEqual(expected, actual);
        }
        [TestCase("USDRUB", 1)]
        [TestCase("EURUSD", 2)]
        [TestCase("RUBEUR", 3)]
        [TestCase("USDJPY", 4)]
        public void ConvertRecipientAccountCurrencyPairToCurrencyPositiveTest(string inputCurrencyPair, Currency expected)
        {
            var actual = Converters.ConvertRecipientAccountCurrencyPairToCurrency(inputCurrencyPair);

            Assert.AreEqual(expected, actual);
        }
        [TestCase("RUBUSD", 1000000, 75000000)]
        [TestCase("JPYEUR", 2000, 266666.6667)]
        [TestCase("USDJPY", 0, 0)]
        public void ConvertAmountPositiveTest(string inputCurrencyPair, decimal amount, decimal expected)
        {
            var actual = Converters.ConvertAmount(inputCurrencyPair, amount);

            Assert.AreEqual(expected, actual);
        }
        [TestCase("USRUB")]
        [TestCase("USLDDRUB")]
        [TestCase("12@#13#@$*23")]
        [TestCase(null)]
        public void ConvertSenderAccountCurrencyPairToCurrencyNegativeTest(string inputCurrencyPair)
        {
            try
            {
                Converters.ConvertSenderAccountCurrencyPairToCurrency(inputCurrencyPair);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [TestCase("USRUB")]
        [TestCase("USLDDRUB")]
        [TestCase("12@#13#@$*23")]
        [TestCase(null)]
        public void ConvertRecipientAccountCurrencyPairToCurrencyNegativeTest(string inputCurrencyPair)
        {
            try
            {
                Converters.ConvertRecipientAccountCurrencyPairToCurrency(inputCurrencyPair);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [TestCase("EURJ", 2000)]
        [TestCase("12@#13#@$*23", 1000000)]
        [TestCase(null, 1000000)]
        [TestCase(null, 0)]
        public void ConvertAmountNegativeTest(string inputCurrencyPair, decimal amount)
        {
            try
            {
                Converters.ConvertAmount(inputCurrencyPair, amount);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
    }
}