using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using TransactionStore.Core.Settings;
using TransactionStore.Core.Models;
using Dapper;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System;

namespace TransactionStore.Data
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public TransactionRepository(IOptions<AppSettings> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public async Task<int> AddDepositeOrWithdrawAsync(SimpleTransactionDto transaction, DateTime? timestamp)
        {
            var result = await _connection
                     .QuerySingleAsync<int>("dbo.Transaction_AddDepositOrWithdraw",
                     new
                     {
                         AccountId = transaction.AccountId,
                         amount = transaction.Amount,
                         currency = (int)transaction.Currency,
                         type = (int)transaction.Type,
                         timestampOld = timestamp
                     },
                     commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<(int, int)> AddTransferAsync(TransferDto transfer, DateTime? timestamp)
        {
            var result = await _connection
                    .QueryFirstOrDefaultAsync<(int, int)>("dbo.Transaction_AddTransfer",
                    new
                    {
                        senderAccountId = transfer.SenderAccountId,
                        recipientAccountId = transfer.RecipientAccountId,
                        senderAmount = transfer.SenderAmount,
                        recipientAmount = transfer.RecipientAmount,
                        senderСurrency = transfer.SenderCurrency,
                        recipientСurrency = transfer.RecipientCurrency,
                        timestampOld = timestamp
                    },
                    commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task <List<SimpleTransactionDto>> GetDepositOrWithdrawByAccountIdsAsync(DataTable accountIds)
        {
            using var connection = new SqlConnection(_connectionString);
            var transactions = (await connection.QueryAsync<SimpleTransactionDto>("dbo.Transaction_SelectByAccountIdsList",
            new { accountIds },
            commandType: CommandType.StoredProcedure)).ToList();
            return transactions;
        }
        public async Task <List<SimpleTransactionDto>> GetDepositOrWithdrawByAccountIdAsync(int accountId)
        {
            var transactions = (await _connection.QueryAsync<SimpleTransactionDto>("dbo.Transaction_SelectByAccountId",
            new { accountId },
            commandType: CommandType.StoredProcedure)).ToList();
            return transactions;
        }
        public async Task <List<TransferDto>> GetTransfersByAccountIdsAsync(DataTable accountIds)
        {
            using var connection = new SqlConnection(_connectionString);
            var transfers = (await connection.QueryAsync<TransferDto>("dbo.Transaction_SelectTransferByAccountIdsList",
               new { accountIds },
               commandType: CommandType.StoredProcedure)).ToList();
            return transfers;
        }
        public async Task<AccountBalanceDto> GetBalanceByAccountIdAsync(int accountId)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.QueryFirstOrDefaultAsync<AccountBalanceDto>("dbo.Transaction_GetBalanceByAccountId",
                    new
                    { accountId },
                    commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<AccountBalanceWithTimestampDto> GetBalanceByAccountIdWithTimestampAsync(int accountId)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.QueryFirstOrDefaultAsync<AccountBalanceWithTimestampDto>("dbo.Transaction_GetBalanceByAccountIdWithTimestamp",
                    new
                    { accountId },
                    commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
