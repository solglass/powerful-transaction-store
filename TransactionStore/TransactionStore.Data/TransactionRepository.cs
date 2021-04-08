using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using TransactionStore.Core.Settings;
using TransactionStore.Core.Models;
using Dapper;
using System.Linq;

namespace TransactionStore.Data
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public TransactionRepository(IOptions<AppSettings> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public int AddDepositeOrWithdraw(SimpleTransactionDto transaction)
        {
            var result = _connection
                     .QuerySingle<int>("dbo.Transaction_AddDepositOrWithdraw",
                     new
                     {
                         leadId = transaction.LeadId,
                         amount = transaction.Amount,
                         currency = (int)transaction.Currency,
                         type = (int)transaction.Type
                     },
                     commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public (int, int) AddTransfer(TransferDto transfer)
        {
            var result = _connection
                    .QueryFirstOrDefault<(int, int)>("dbo.Transaction_AddTransfer",
                    new
                    {
                        senderId = transfer.SenderId,
                        recipientId = transfer.RecipientId,
                        senderAmount = transfer.SenderAmount,
                        recipientAmount = transfer.RecipientAmount,
                        senderСurrency = transfer.SenderCurrency,
                        recipientСurrency = transfer.RecipientCurrency
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<SimpleTransactionDto> GetDepositOrWithdrawByLeadId(int leadId)
        {
            var transactions =
                _connection.Query<SimpleTransactionDto>("dbo.Transaction_SelectByLeadId",
            new { leadId},
            commandType: System.Data.CommandType.StoredProcedure).ToList();
            return transactions;
        }
        public List<TransferDto> GetTransfersByLeadId(int leadId)
        {
            var transfers =
                _connection.Query<TransferDto>("dbo.Transaction_SelectTransferByLeadId",
                new { leadId },
                commandType: System.Data.CommandType.StoredProcedure).ToList();
            return transfers;

        }
        public List<LeadBalanceDto> GetBalanceByLeadId(int leadId)
        {
            var result = _connection
                    .Query<LeadBalanceDto>("dbo.Transaction_GetBalanceByLeadId",
                    new
                    {
                        leadId = leadId
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                    ).ToList();
            return result;
        }
    }
}
