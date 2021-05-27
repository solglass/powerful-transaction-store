using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TransactionStore.Core.Models;

namespace TransactionStore.Data
{
    public interface ITransactionRepository
    {
        Task<int> AddDepositeOrWithdrawAsync(SimpleTransactionDto dto, DateTime timestamp);
        Task<(int, int)> AddTransferAsync(TransferDto dto, DateTime timestamp);
        Task <List<SimpleTransactionDto>> GetDepositOrWithdrawByAccountIdsAsync(DataTable accountIds);
        Task<List<TransferDto>> GetTransfersByAccountIdsAsync(DataTable accountIds);
        Task<List<SimpleTransactionDto>> GetDepositOrWithdrawByAccountIdAsync(int accountId);
        Task<AccountBalanceDto> GetBalanceByAccountIdAsync(int accountId);
        Task<AccountBalanceWithTimestampDto> GetBalanceByAccountIdWithTimestampAsync(int accountId);
    }
}
