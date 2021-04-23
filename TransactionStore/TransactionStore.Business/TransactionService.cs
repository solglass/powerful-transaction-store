using TransactionStore.Data;
using TransactionStore.Core.Models;
using System.Collections.Generic;
using TransactionStore.Core.Enums;
using TransactionStore.Core.Utils;
using System;

namespace TransactionStore.Business
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public int AddDeposite(SimpleTransactionDto transaction)
        {
            transaction.Type = (TransactionType)1;
            return _transactionRepository.AddDepositeOrWithdraw(transaction);
        }
        public int AddWithdraw(SimpleTransactionDto transaction)
        {
            transaction.Type = (TransactionType)2;
            return _transactionRepository.AddDepositeOrWithdraw(transaction);
        }
        public (int, int) AddTransfer(TransferDto transfer) => _transactionRepository.AddTransfer(transfer);
        public List<BaseTransactionDto> GetTransactionsByAccountId(int accountId)
        {
            var depositesOrWithdraws = _transactionRepository.GetDepositOrWithdrawByAccountId(accountId).ConvertAll(x => (BaseTransactionDto)x);
            var transfers = _transactionRepository.GetTransfersByAccountId(accountId).ConvertAll(x => (BaseTransactionDto)x);
            var transactions = new List<BaseTransactionDto>();
            transactions.AddRange(depositesOrWithdraws);
            transactions.AddRange(transfers);
            transactions.ConvertAll(transactions => Decimal.Round(transactions.Amount, 2));
            return transactions;
        }

        public List<AccountBalanceDto> GetBalance(List<int> accounts, string currancy)
        {
            var balance = new List<AccountBalanceDto>();
            var wholeBalance = new AccountBalanceDto();
            foreach(var account in accounts)
            {
                var balanceDto = _transactionRepository.GetBalanceByAccountId(account);
                balance.Add(balanceDto);
                wholeBalance.Amount += Converters.ConvertAmount(balanceDto.Currency + currancy, balanceDto.Amount);
            }
            wholeBalance.Currency = (Currency)Enum.Parse(typeof(Currency), currancy);
            balance.Add(wholeBalance);
            return balance;
        }
    }
}
