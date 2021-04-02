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

        List<TransactionDto> ITransactionRepository.GetTransactionByLeadId(int leadId)
        {
            throw new NotImplementedException();
        }

        public decimal GetBalanceByLeadId(int leadId)
        {
            throw new NotImplementedException();
        }
    }
}
