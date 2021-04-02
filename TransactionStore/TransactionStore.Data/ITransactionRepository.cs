using System;
using System.Collections.Generic;
using TransactionStore.Core.Models;

namespace TransactionStore.Data
{
    public interface ITransactionRepository
    {
        int AddDepositeOrWithdraw(TransactionDto dto);
        int AddTransfer(TransferDto dto);
        List<TransactionDto> GetTransactionByLeadId(int leadId);
        decimal GetBalanceByLeadId(int leadId);
    }
}
