using NUnit.Framework;
using TransactionStore.Core.Utils;
using TransactionStore.Core.Enums;

namespace TransactionStore.Test
{
    public class CurrancyConverterTests
    {
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
        [TestCase("USDRUB", 1000000, 75000000)]
        [TestCase("EURJPY", 2000, 266666.6667)]
        [TestCase("JPYUSD", 0, 0)]
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