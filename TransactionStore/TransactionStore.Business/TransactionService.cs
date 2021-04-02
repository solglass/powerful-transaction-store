using System;
using TransactionStore.Data;
using TransactionStore.Core.Models;
using System.Collections.Generic;

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
        public List<TransactionDto> GetTransactionsByLeadId(int leadId)
        {
            throw new NotImplementedException();
        }

        public decimal GetBalanceByLeadId(int leadId)
        {
            throw new NotImplementedException();
        }
    }
}
