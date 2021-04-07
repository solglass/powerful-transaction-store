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

        public int AddDeposite(SimpleTransactionDto transaction)
        {
            transaction.Type = (TransactionType)1;
            return _transactionRepository.AddDepositeOrWithdraw(transaction);
        }
        public int AddWithdraw(SimpleTransactionDto transaction)
        {
            transaction.Type = (TransactionType)2;
            return _transactionRepository.AddDepositeOrWithdraw(transaction);
        }
        public int AddTransfer(TransferDto transfer) => _transactionRepository.AddTransfer(transfer);
        public List<BaseTransactionDto> GetTransactionsByLeadId(int leadId)
        {
            var result = _transactionRepository.GetDepositOrWithdrawByLeadId(leadId, 1).ConvertAll(x => (BaseTransactionDto)x);
            var withdrawTransactionDto = _transactionRepository.GetDepositOrWithdrawByLeadId(leadId, 1).ConvertAll(x => (BaseTransactionDto)x);
            var transfersTransactionDto = _transactionRepository.GetTransfersByLeadId(leadId).ConvertAll(x => (BaseTransactionDto)x);

            result.AddRange(withdrawTransactionDto);
            result.AddRange(transfersTransactionDto);
            return result;
        }
       
        public List<LeadBalanceDto> GetBalanceByLeadId(int leadId) => _transactionRepository.GetBalanceByLeadId(leadId);
    }
}
