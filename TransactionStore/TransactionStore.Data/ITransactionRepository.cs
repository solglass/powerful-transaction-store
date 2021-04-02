using System;
using TransactionStore.Data.Models;

namespace TransactionStore.Data
{
    public interface ITransactionRepository
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
