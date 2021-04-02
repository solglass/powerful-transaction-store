using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TransactionStore.Core.Settings;
using TransactionStore.Core.Models;

namespace TransactionStore.Data
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public TransactionRepository(IOptions<AppSettings> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

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
