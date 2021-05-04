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

        public WholeBalanceDto GetBalance(List<int> accounts, string currency)
        {
            var wholeBalance = new WholeBalanceDto();
            wholeBalance.Accounts = new List<AccountBalanceDto>();
            for (int i = 0; i < accounts.Count; i++)
            {
                wholeBalance.Accounts.Add(_transactionRepository.GetBalanceByAccountId(accounts[i]));
                wholeBalance.Balance += Converters.ConvertAmount(wholeBalance.Accounts[i].Currency + currency, wholeBalance.Accounts[i].Amount);
            }
            wholeBalance.Currency = (Currency)Enum.Parse(typeof(Currency), currency);
            return wholeBalance;
        }
    }
}
