using System;
using TransactionStore.Data;
using TransactionStore.Core.Models;
using System.Collections.Generic;
using EducationSystem.Core.Enums;

namespace TransactionStore.Business
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public int AddDeposite(TransactionDto transaction)
        {
            transaction.Type = (TransactionType)1;
            return _transactionRepository.AddDepositeOrWithdraw(transaction);
        }
        public int AddWithdraw(TransactionDto transaction)
        {
            transaction.Type = (TransactionType)2;
            return _transactionRepository.AddDepositeOrWithdraw(transaction);
        }
        public int AddTransfer(TransferDto transfer) => _transactionRepository.AddTransfer(transfer);
        public List<TransactionDto> GetTransactionsByLeadId(int leadId) => _transactionRepository.GetTransactionsByLeadId(leadId);
        public List<TransferDto> GetTransfersByLeadId(int leadId) => _transactionRepository.GetTransfersByLeadId(leadId);
        public List<LeadBalanceDto> GetBalanceByLeadId(int leadId) => _transactionRepository.GetBalanceByLeadId(leadId);
    }
}
