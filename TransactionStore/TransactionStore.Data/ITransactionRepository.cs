using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TransactionStore.Core.Models;

namespace TransactionStore.Data
{
    public interface ITransactionRepository
    {
        Task<int> AddDepositeOrWithdraw(SimpleTransactionDto dto);
        Task<(int, int)> AddTransfer(TransferDto dto);
        Task <List<SimpleTransactionDto>> GetDepositOrWithdrawByAccountIds(DataTable accountIds);
        Task<List<TransferDto>> GetTransfersByAccountIds(DataTable accountIds);
        Task<List<SimpleTransactionDto>> GetDepositOrWithdrawByAccountId(int accountId);
        Task<AccountBalanceDto> GetBalanceByAccountId(int accountId);
    }
}
