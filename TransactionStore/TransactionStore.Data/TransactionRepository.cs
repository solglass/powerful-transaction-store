﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TransactionStore.Core.Settings;
using TransactionStore.Core.Models;
using Dapper;
using EducationSystem.Core.Enums;
using System.Linq;

namespace TransactionStore.Data
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public TransactionRepository(IOptions<AppSettings> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public int AddDepositeOrWithdraw(TransactionDto transaction)
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

        public int AddTransfer(TransferDto transfer)
        {
            var result = _connection
                    .QuerySingle<int>("dbo.Transaction_AddTransfer",
                    new
                    {
                        senderId = transfer.SenderId,
                        recipientId = transfer.RecipientId,
                        amount = transfer.Amount,
                        currency = transfer.Currency
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<TransactionDto> GetTransactionsByLeadId(int leadId)
        {
            var transactions = 
                _connection.Query<TransactionDto, int, int, TransactionDto>("dbo.Transaction_SelectByLeadId",
           (transaction, type, currency) =>
           {
               transaction.Type = (TransactionType)type;
               transaction.Currency = (Currency)currency;
               return transaction;
           },
            new { leadId }, 
            commandType: System.Data.CommandType.StoredProcedure).ToList();
            return transactions;
        }
        public List<TransferDto> GetTransfersByLeadId(int leadId)
        {
            var transfers =
                _connection.Query<TransferDto, int, int, TransferDto>("dbo.Transaction_SelectTransferByLeadId",
           (transfer, type, currency) =>
           {
               transfer.Type = (TransactionType)type;
               transfer.Currency = (Currency)currency;
               return transfer;
           },
            new { leadId },
            commandType: System.Data.CommandType.StoredProcedure).ToList();
            return transfers;
        }
        public decimal GetBalanceByLeadId(int leadId)
        {
            var result = _connection
                    .QuerySingle<decimal>("dbo.Transaction_GetBalanceByLeadId",
                    new
                    {
                        leadId = leadId
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
