using TransactionStore.Data;
using TransactionStore.Core.Models;
using System.Collections.Generic;
using TransactionStore.Core.Enums;
using System;

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
        public (int, int) AddTransfer(TransferDto transfer) => _transactionRepository.AddTransfer(transfer);
        public List<BaseTransactionDto> GetTransactionsByLeadId(int leadId)
        {
            var depositesOrWithdraws = _transactionRepository.GetDepositOrWithdrawByLeadId(leadId).ConvertAll(x => (BaseTransactionDto)x);
            var transfers = _transactionRepository.GetTransfersByLeadId(leadId).ConvertAll(x => (BaseTransactionDto)x);
            var transactions = new List<BaseTransactionDto>();
            transactions.AddRange(depositesOrWithdraws);
            transactions.AddRange(transfers);
            transactions.ConvertAll(transactions => Decimal.Round(transactions.Amount, 2));
            return transactions;
        }

        public List<LeadBalanceDto> GetBalanceByLeadId(int leadId) 
        {
            var balances = _transactionRepository.GetBalanceByLeadId(leadId);
            balances.ConvertAll(balance => Decimal.Round(balance.Amount, 2));
            return balances;
        }
    }
}
