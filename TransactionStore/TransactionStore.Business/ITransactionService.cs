using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionStore.Core.Models;

namespace TransactionStore.Business
{
    public interface ITransactionService
    {
        Task<int> AddDepositeAsync(SimpleTransactionDto dto, AccountBalanceWithTimestampDto balance);
        Task<int> AddWithdrawAsync(SimpleTransactionDto dto, DateTime? timestamp);
        Task<(int, int)> AddTransferAsync(TransferDto dto, DateTime? timestamp);
        Task<List<BaseTransactionDto>> GetTransactionsByAccountIdsAsync(List<int> AccountIds);
        Task<WholeBalanceDto> GetBalanceAsync(List<int> accounts, string currancy);
        Task<AccountBalanceWithTimestampDto> GetBalanceWithTimestampAsync(int accountId);
        Task<decimal> ConvertAmount(string senderCurrency, string recipientCurrency, AccountBalanceWithTimestampDto balance);
    }
}
