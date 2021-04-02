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
        int DeleteTransaction(int transactionId);
        TransactionDto GetTransactionById(int transactionId);
        TransactionDto GetTransactionByLeadId(int transactionId);
        TransferDto GetTransferBydId(int transactionId);
        LeadBalanceDto GetBalanceByLeadId(int leadId, DateTime date);
    }
}
