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

        public async Task<int> AddDepositeAsync(SimpleTransactionDto transaction)
        {
            transaction.Amount = _converterService.ConvertAmount(transaction.ValueCurrency.ToString(), transaction.Currency.ToString(), transaction.Amount);
            transaction.Type = (TransactionType)1;
            var result = await _transactionRepository.AddDepositeOrWithdrawAsync(transaction);
            return result;
        }

        public async Task<int> AddWithdrawAsync(SimpleTransactionDto transaction)
        {
            transaction.Amount = _converterService.ConvertAmount(transaction.ValueCurrency.ToString(), transaction.Currency.ToString(), transaction.Amount);
            transaction.Type = (TransactionType)2;
            var result = await _transactionRepository.AddDepositeOrWithdrawAsync(transaction);
            return result;
        }

        public async Task<(int, int)> AddTransferAsync(TransferDto transfer) 
        {
            transfer.RecipientAmount = _converterService.ConvertAmount(transfer.SenderCurrency.ToString(), transfer.RecipientCurrency.ToString(), transfer.RecipientAmount);
            var result = await _transactionRepository.AddTransferAsync(transfer);
            return result;
        }

        public async Task<List<BaseTransactionDto>> GetTransactionsByAccountIdsAsync(List<int> accountIds)
        {
            var dataTable = _converterService.ConvertListToDataTable(accountIds);
            var depositesOrWithdraws = _transactionRepository.GetDepositOrWithdrawByAccountIdsAsync(dataTable);
            var transfers = _transactionRepository.GetTransfersByAccountIdsAsync(dataTable);
            await Task.WhenAll(new Task[] { depositesOrWithdraws, transfers });
            var transactions = new List<BaseTransactionDto>();
            transactions.AddRange(depositesOrWithdraws.Result);
            transactions.AddRange(transfers.Result);
            transactions.ConvertAll(transactions => Decimal.Round(transactions.Amount, 2));
            return transactions;
        }

        //public async Task<WholeBalanceDto> GetBalanceAsync(List<int> accounts, string currency)
        //{
        //    var wholeBalance = new WholeBalanceDto();
        //    wholeBalance.Accounts = new List<AccountBalanceDto>();
        //    var tasks = new List<Task<AccountBalanceDto>>();
        //    foreach (var account in accounts)
        //    {
        //        var accountBalanceDto = _transactionRepository.GetBalanceByAccountIdAsync(account);
        //        if (accountBalanceDto.Result is null)
        //        {
        //            wholeBalance.Accounts.Add(new AccountBalanceDto()
        //            { AccountId = account, Amount = 0, Currency = null });
        //        }
        //        else
        //        {
        //            accountBalanceDto.Result.AccountId = account;
        //            tasks.Add(accountBalanceDto);
        //            wholeBalance.Accounts.Add(accountBalanceDto.Result);
        //        }
        //    }
        //    await Task.WhenAll(tasks);
        //    ProccessWholeBalanceAsync(wholeBalance, currency);
        //    return wholeBalance;
        //}
        public async Task<WholeBalanceDto> GetBalanceAsync(List<int> accounts, string currency)
        {
            var wholeBalance = new WholeBalanceDto();
            wholeBalance.Accounts = new List<AccountBalanceDto>();
            var tasks = new List<Task<AccountBalanceDto>>();
            for (int i = 0; i < tasks.Count; i++)
            {
                var accountBalanceDto = _transactionRepository.GetBalanceByAccountIdAsync(accounts[i]);
                tasks.Add(accountBalanceDto);
            }
            await Task.WhenAll(tasks);
            for (int i = 0; i < accounts.Count; i++)
            {

                if (tasks[i].Result is null)
                {
                    wholeBalance.Accounts.Add(new AccountBalanceDto()
                    { AccountId = accounts[i], Amount = 0, Currency = null });
                }
                else
                {
                    tasks[i].Result.AccountId = accounts[i];
                    wholeBalance.Accounts.Add(tasks[i].Result);
                }
            }
            ProccessWholeBalanceAsync(wholeBalance, currency);
            return wholeBalance;
        }

        private void ProccessWholeBalanceAsync(WholeBalanceDto wholeBalance, string currency)
        {
            for (int i = 0; i < wholeBalance.Accounts.Count; i++)
            {
                if (wholeBalance.Accounts[i].Currency != null)
                 wholeBalance.Balance += _converterService.ConvertAmount(wholeBalance.Accounts[i].Currency.ToString(), 
                 currency, wholeBalance.Accounts[i].Amount);
            }
            wholeBalance.Currency = _converterService.ConvertCurrencyStringToCurrencyEnum(currency);
        }
    }
}
