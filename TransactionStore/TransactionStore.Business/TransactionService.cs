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

        public async Task<int> AddDeposite(SimpleTransactionDto transaction)
        {
            transaction.Amount = _converterService.ConvertAmount(transaction.ValueCurrency.ToString(), transaction.Currency.ToString(), transaction.Amount);
            transaction.Type = (TransactionType)1;
            var result = await _transactionRepository.AddDepositeOrWithdraw(transaction);
            return result;
        }
        public async Task<int> AddWithdraw(SimpleTransactionDto transaction)
        {
            transaction.Amount = _converterService.ConvertAmount(transaction.ValueCurrency.ToString(), transaction.Currency.ToString(), transaction.Amount);
            transaction.Type = (TransactionType)2;
            var result = await _transactionRepository.AddDepositeOrWithdraw(transaction);
            return result;
        }
        public async Task<(int, int)> AddTransfer(TransferDto transfer) 
        {
            transfer.RecipientAmount = _converterService.ConvertAmount(transfer.SenderCurrency.ToString(), transfer.RecipientCurrency.ToString(), transfer.RecipientAmount);
            var result = await _transactionRepository.AddTransfer(transfer);
            return result;
        }
        
        public async Task<List<BaseTransactionDto>> GetTransactionsByAccountIds(List<int> accountIds)
        {
            var dataTable = _converterService.ConvertListToDataTable(accountIds);
            var depositesOrWithdraws = await _transactionRepository.GetDepositOrWithdrawByAccountIds(dataTable);
            var transfers = await _transactionRepository.GetTransfersByAccountIds(dataTable);
            depositesOrWithdraws.ConvertAll(x => (BaseTransactionDto)x);
            transfers.ConvertAll(x => (BaseTransactionDto)x);
            var transactions = new List<BaseTransactionDto>();
            transactions.AddRange(depositesOrWithdraws);
            transactions.AddRange(transfers);
            transactions.ConvertAll(transactions => Decimal.Round(transactions.Amount, 2));
            return transactions;
        }

        public async Task<WholeBalanceDto> GetBalance(List<int> accounts, string currency)
        {
            var wholeBalance = new WholeBalanceDto();
            wholeBalance.Accounts = new List<AccountBalanceDto>();
            for (int i = 0; i < accounts.Count; i++)
            {
                var accountBalanceDto = await _transactionRepository.GetBalanceByAccountId(accounts[i]);
                if (accountBalanceDto is null)
                    wholeBalance.Accounts.Add(new AccountBalanceDto() { AccountId = accounts[i], Amount = 0, Currency = null});
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
    }
}
