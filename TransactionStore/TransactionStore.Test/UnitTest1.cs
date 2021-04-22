using NUnit.Framework;
using TransactionStore.API.Utils;
using TransactionStore.Core.Enums;

namespace TransactionStore.Test
{
    public class Tests
    {
        [TestCase("RUBUSD", 1)]
        [TestCase("USDRUB", 2)]
        [TestCase("EURRUB", 3)]
        [TestCase("JPYEUR", 4)]
        public void ConvertSenderCurrencyPairToCurrencyPositiveTest(string inputCurrencyPair, Currency expected)
        {
            var actual = Converters.ConvertSenderCurrencyPairToCurrency(inputCurrencyPair);

            Assert.AreEqual(expected, actual);
        }
        [TestCase("USDRUB", 1)]
        [TestCase("EURUSD", 2)]
        [TestCase("RUBEUR", 3)]
        [TestCase("USDJPY", 4)]
        public void ConvertRecipientCurrencyPairToCurrencyPositiveTest(string inputCurrencyPair, Currency expected)
        {
            var actual = Converters.ConvertRecipientCurrencyPairToCurrency(inputCurrencyPair);

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
        public void ConvertSenderCurrencyPairToCurrencyNegativeTest(string inputCurrencyPair)
        {
            try
            {
                Converters.ConvertSenderCurrencyPairToCurrency(inputCurrencyPair);
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
        public void ConvertRecipientCurrencyPairToCurrencyNegativeTest(string inputCurrencyPair)
        {
            try
            {
                Converters.ConvertRecipientCurrencyPairToCurrency(inputCurrencyPair);
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