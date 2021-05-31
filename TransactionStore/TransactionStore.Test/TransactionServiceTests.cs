using NUnit.Framework;
using Moq;
using TransactionStore.Core.Enums;
using System.Collections.Generic;
using TransactionStore.Business;
using TransactionStore.Data;
using TransactionStore.Core.Models;

namespace TransactionStore.Test
{
    public class TransactionServiceTests
    {
        private Mock<ITransactionRepository> _transactionRepositoryMock;
        private TransactionService _transactionService;
        private CurrencyRatesService _currencyRatesService;
        private ConverterService _converterService;
        [SetUp]
        public void SetUp()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _currencyRatesService = new CurrencyRatesService();
            _converterService = new ConverterService(_currencyRatesService);
            _currencyRatesService.CurrencyPair = new Dictionary<string, decimal>();
            _transactionService = new TransactionService(_transactionRepositoryMock.Object, _converterService);
            _currencyRatesService.CurrencyPair = new Dictionary<string, decimal>()
            {
                ["USDUSD"] = 1,
                ["USDRUB"] = 74,
                ["USDEUR"] = 0.84m,
                ["USDJPY"] = 0.009m
            };
        }
        [Test]
        public void GetBalanceAsyncPositiveTest()
        {
            var expected = new WholeBalanceDto();
            expected.Accounts = new List<AccountBalanceDto>();
            var currency = "RUB";

            var accounts = new List<int>() { 51,52,60};
            var dto1 = new AccountBalanceDto()
            {
                AccountId = 51,
                Amount = -100,
                Currency = Currency.RUB
            };
            expected.Accounts.Add(dto1);
            var dto2 = new AccountBalanceDto()
            {
                AccountId = 52,
                Amount = 0,
                Currency = null
            };
            expected.Accounts.Add(dto2);
            var dto3 = new AccountBalanceDto()
            {
                AccountId = 60,
                Amount = 10,
                Currency = Currency.EUR
            };
            expected.Accounts.Add(dto3);

            for(int i = 0; i < accounts.Count; i++)
            {
                _transactionRepositoryMock.Setup((mock) => mock.GetBalanceByAccountIdAsync(accounts[i]).Result).Returns(expected.Accounts[i]);
            }
            expected.Balance = 780.9524m;
            expected.Currency = Currency.RUB;

            var actual = _transactionService.GetBalanceAsync(accounts, currency).Result;

            CollectionAssert.AreEqual(expected.Accounts, actual.Accounts);
            Assert.AreEqual(expected.Balance, actual.Balance);
            Assert.AreEqual(expected.Currency, actual.Currency);
        }
    
    }
}