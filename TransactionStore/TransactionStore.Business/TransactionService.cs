using TransactionStore.Data;
using TransactionStore.Core.Models;
using System.Collections.Generic;
using TransactionStore.Core.Enums;
using System;
using System.Threading.Tasks;

namespace TransactionStore.Business
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;
        private ConverterService _converterService;

        public TransactionService(ITransactionRepository transactionRepository, ConverterService converterService)
        {
            _transactionRepository = transactionRepository;
            _converterService = converterService;
        }

        public int AddDeposite(SimpleTransactionDto transaction)
        {
            transaction.Amount = _converterService.ConvertAmount(transaction.ValueCurrency.ToString(), transaction.Currency.ToString(), transaction.Amount);
            transaction.Type = (TransactionType)1;
            return _transactionRepository.AddDepositeOrWithdraw(transaction);
        }

        public async Task<int> AddDepositeAsync(SimpleTransactionDto transaction)
        {
            return await Task.Run(() => AddDeposite(transaction));
        }
        public int AddWithdraw(SimpleTransactionDto transaction)
        {
            transaction.Amount = _converterService.ConvertAmount(transaction.ValueCurrency.ToString(), transaction.Currency.ToString(), transaction.Amount);
            transaction.Type = (TransactionType)2;
            return _transactionRepository.AddDepositeOrWithdraw(transaction);
        }

        public async Task<int> AddWithdrawAsync(SimpleTransactionDto transaction)
        {
            return await Task.Run(() => AddWithdraw(transaction));
        }
        public (int, int) AddTransfer(TransferDto transfer)
        {
            transfer.RecipientAmount = _converterService.ConvertAmount(transfer.SenderCurrency.ToString(), transfer.RecipientCurrency.ToString(), transfer.RecipientAmount);
            return _transactionRepository.AddTransfer(transfer);
        }

        public async Task<(int, int)> AddTransferAsync(TransferDto transfer)
        {
            return await Task.Run(() => AddTransfer(transfer));
        }
        public List<BaseTransactionDto> GetTransactionsByAccountIds(List<int> accountIds)
        {
            var dataTable = _converterService.ConvertListToDataTable(accountIds);
            var depositesOrWithdraws = _transactionRepository.GetDepositOrWithdrawByAccountIds(dataTable).ConvertAll(x => (BaseTransactionDto)x);
            var transfers = _transactionRepository.GetTransfersByAccountIds(dataTable).ConvertAll(x => (BaseTransactionDto)x);
            var transactions = new List<BaseTransactionDto>();
            transactions.AddRange(depositesOrWithdraws);
            transactions.AddRange(transfers);
            transactions.ConvertAll(transactions => Decimal.Round(transactions.Amount, 2));
            return transactions;
        }

        public async Task<List<BaseTransactionDto>> GetTransactionsByAccountIdsAsync(List<int> accountIds)
        {
            return await Task.Run(() => GetTransactionsByAccountIds(accountIds));
        }

        public bool GetTransactionsByAccountId(int accountId)
        {
            if (_transactionRepository.GetDepositOrWithdrawByAccountId(accountId) is null)
                return false;
            return true;
        }

        public async Task<bool> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await Task.Run(() => GetTransactionsByAccountId(accountId));
        }

        public WholeBalanceDto GetBalance(List<int> accounts, string currency)
        {
            var wholeBalance = new WholeBalanceDto();
            wholeBalance.Accounts = new List<AccountBalanceDto>();
            for (int i = 0; i < accounts.Count; i++)
            {
                var accountBalanceDto = _transactionRepository.GetBalanceByAccountId(accounts[i]);
                if (accountBalanceDto is null)
                    wholeBalance.Accounts.Add(new AccountBalanceDto() { AccountId = accounts[i], Amount = 0, Currency = null });
                else
                {
                    accountBalanceDto.AccountId = accounts[i];
                    wholeBalance.Accounts.Add(accountBalanceDto);
                    wholeBalance.Balance += _converterService.ConvertAmount(wholeBalance.Accounts[i].Currency.ToString(), currency, wholeBalance.Accounts[i].Amount);
                }
            }
            wholeBalance.Currency = _converterService.ConvertCurrencyStringToCurrencyEnum(currency);
            return wholeBalance;
        }

        public async Task<WholeBalanceDto> GetBalanceAsync(List<int> accounts, string currency)
        {
            return await Task.Run(() => GetBalance(accounts, currency));
        }
    }
}
