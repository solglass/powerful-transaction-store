using System;
using TransactionStore.Data;
using TransactionStore.Core.Models;
using System.Collections.Generic;

namespace TransactionStore.Business
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;

        public int AddDepositeOrWithdraw(TransactionDto transaction)
        {
            return _transactionRepository.AddDepositeOrWithdraw(transaction);
        }

        public int AddTransfer(TransferDto transfer)
        {
            return _transactionRepository.AddTransfer(transfer);
        }
        public List<TransactionDto> GetTransactionsByLeadId(int leadId)
        {
            return _transactionRepository.GetTransactionsByLeadId(leadId);
        }
        public List<TransferDto> GetTransfersByLeadId(int leadId)
        {
            return _transactionRepository.GetTransfersByLeadId(leadId);
        }

        public decimal GetBalanceByLeadId(int leadId)
        {
            return _transactionRepository.GetBalanceByLeadId(leadId);
        }
    }
}
