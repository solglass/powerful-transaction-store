using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionStore.Core.Models;

namespace TransactionStore.Business
{
    public interface ITransactionService
    {
        Task<int> AddDepositeAsync(SimpleTransactionDto dto);
        Task<int> AddWithdrawAsync(SimpleTransactionDto dto);
        Task<(int, int)> AddTransferAsync(TransferDto dto, DateTime timestamp);
        Task<List<BaseTransactionDto>> GetTransactionsByAccountIdsAsync(List<int> AccountIds);
        Task<WholeBalanceDto> GetBalanceAsync(List<int> accounts, string currancy);
        Task<AccountBalanceWithTimestampDto> GetBalanceWithTimestampAsync(int accountId);
    }
}
