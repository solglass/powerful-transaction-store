using System.Collections.Generic;
using System.Data;
using TransactionStore.Core.Models;

namespace TransactionStore.Data
{
    public interface ITransactionRepository
    {
        int AddDepositeOrWithdraw(SimpleTransactionDto dto);
        (int, int) AddTransfer(TransferDto dto);
        List<SimpleTransactionDto> GetDepositOrWithdrawByAccountIds(DataTable accountIds);
        List<TransferDto> GetTransfersByAccountIds(DataTable accountIds);
        List<SimpleTransactionDto> GetDepositOrWithdrawByAccountId(int accountId);
        AccountBalanceDto GetBalanceByAccountId(int accountId);
    }
}
