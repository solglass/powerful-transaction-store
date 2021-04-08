using System;
using System.Collections.Generic;
using System.Text;
using TransactionStore.Core.Models;

namespace TransactionStore.Business
{
    public interface ITransactionService
    {
        int AddDeposite(SimpleTransactionDto dto);
        int AddWithdraw(SimpleTransactionDto dto);
        (int, int) AddTransfer(TransferDto dto);
        List<BaseTransactionDto> GetTransactionsByLeadId(int leadId);
        List <LeadBalanceDto> GetBalanceByLeadId(int leadId);
    }
}
