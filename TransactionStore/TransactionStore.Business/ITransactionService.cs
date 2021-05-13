using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionStore.Core.Models;

namespace TransactionStore.Business
{
    public interface ITransactionService
    {
        Task<int> AddDeposite(SimpleTransactionDto dto);
        Task<int> AddWithdraw(SimpleTransactionDto dto);
        Task<(int, int)> AddTransfer(TransferDto dto);
        Task<List<BaseTransactionDto>> GetTransactionsByAccountIds(List<int> AccountIds);
        Task<WholeBalanceDto> GetBalance(List<int> accounts, string currancy);
    }
}
