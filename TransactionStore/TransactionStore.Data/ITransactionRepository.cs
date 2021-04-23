using System.Collections.Generic;
using TransactionStore.Core.Models;

namespace TransactionStore.Data
{
    public interface ITransactionRepository
    {
        int AddDepositeOrWithdraw(SimpleTransactionDto dto);
        (int, int) AddTransfer(TransferDto dto);
        List<SimpleTransactionDto> GetDepositOrWithdrawByAccountId(int accountId);
        List<TransferDto> GetTransfersByAccountId(int accountId);
        AccountBalanceDto GetBalanceByAccountId(int accountId);
    }
}
