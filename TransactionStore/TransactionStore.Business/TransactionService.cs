using System;
using TransactionStore.Data;
using TransactionStore.Core.Models;
using System.Collections.Generic;

namespace TransactionStore.Business
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public int AddDepositeOrWithdraw(TransactionDto transaction) => _transactionRepository.AddDepositeOrWithdraw(transaction);
        public int AddTransfer(TransferDto transfer) => _transactionRepository.AddTransfer(transfer);
        public List<TransactionDto> GetTransactionsByLeadId(int leadId) => _transactionRepository.GetTransactionsByLeadId(leadId);
        public List<TransferDto> GetTransfersByLeadId(int leadId) => _transactionRepository.GetTransfersByLeadId(leadId);
        public decimal GetBalanceByLeadId(int leadId) => _transactionRepository.GetBalanceByLeadId(leadId);
    }
}
