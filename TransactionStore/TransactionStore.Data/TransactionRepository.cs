using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using TransactionStore.Core.Settings;
using TransactionStore.Core.Models;
using Dapper;
using System.Linq;
using System.Data;
using TransactionStore.Core.Enums;

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
                         AccountId = transaction.AccountId,
                         amount = transaction.Amount,
                         currency = (int)transaction.Currency,
                         type = (int)transaction.Type
                     },
                     commandType: CommandType.StoredProcedure);
            return result;
        }

        public (int, int) AddTransfer(TransferDto transfer)
        {
            var result = _connection
                    .QueryFirstOrDefault<(int, int)>("dbo.Transaction_AddTransfer",
                    new
                    {
                        senderAccountId = transfer.SenderAccountId,
                        recipientAccountId = transfer.RecipientAccountId,
                        senderAmount = transfer.SenderAmount,
                        recipientAmount = transfer.RecipientAmount,
                        senderСurrency = transfer.SenderCurrency,
                        recipientСurrency = transfer.RecipientCurrency
                    },
                    commandType: CommandType.StoredProcedure);
            return result;
        }

        public List<SimpleTransactionDto> GetDepositOrWithdrawByAccountId(int accountId)
        {
            var transactions =
                _connection.Query<SimpleTransactionDto>("dbo.Transaction_SelectByAccountId",
            new { accountId },
            commandType: CommandType.StoredProcedure).ToList();
            return transactions;
        }
        public List<TransferDto> GetTransfersByAccountId(int accountId)
        {
            var transfers =
                _connection.Query<TransferDto>("dbo.Transaction_SelectTransferByAccountId",
                new { accountId },
                commandType: CommandType.StoredProcedure).ToList();
            return transfers;

        }
        public AccountBalanceDto GetBalanceByAccountId(int accountId)
        {
            var result = _connection
                    .QueryFirstOrDefault<AccountBalanceDto>("dbo.Transaction_GetBalanceByAccountId",
                    new
                    {
                        accountId
                    },
                    commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
