using System;
using TransactionStore.Data;
using TransactionStore.Data.Models;

namespace TransactionStore.Business
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;

        public int AddDepositeOrWithdraw(TransactionDto dto)
        {
            throw new NotImplementedException();
        }

        public int AddTransfer(TransferDto dto)
        {
            throw new NotImplementedException();
        }

        public int DeleteTransaction(int transactionId)
        {
            throw new NotImplementedException();
        }

        public LeadBalanceDto GetBalanceByLeadId(int leadId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public TransactionDto GetTransactionById(int transactionId)
        {
            throw new NotImplementedException();
        }

        public TransactionDto GetTransactionByLeadId(int transactionId)
        {
            throw new NotImplementedException();
        }

        public TransferDto GetTransferBydId(int transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
