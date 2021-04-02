using System;
using System.Collections.Generic;
using System.Text;
using TransactionStore.Core.Models;

namespace TransactionStore.Business
{
    public interface ITransactionService
    {
        int AddDepositeOrWithdraw(TransactionDto dto);
        int AddTransfer(TransferDto dto);
        List<TransactionDto> GetTransactionsByLeadId(int leadId);
        decimal GetBalanceByLeadId(int leadId);
    }
}
