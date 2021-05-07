using System.Collections.Generic;
using TransactionStore.Core.Models;

namespace TransactionStore.Business
{
    public interface ITransactionService
    {
        int AddDeposite(SimpleTransactionDto dto);
        int AddWithdraw(SimpleTransactionDto dto);
        (int, int) AddTransfer(TransferDto dto);
        List<BaseTransactionDto> GetTransactionsByAccountIds(List<int> AccountIds);
        public bool GetTransactionsByAccountId(int accountId);
        WholeBalanceDto GetBalance(List<int> accounts, string currancy);
    }
}
