﻿using System;
using System.Collections.Generic;
using TransactionStore.Core.Models;

namespace TransactionStore.Data
{
    public interface ITransactionRepository
    {
        int AddDepositeOrWithdraw(TransactionDto dto);
        int AddTransfer(TransferDto dto);
        List<TransactionDto> GetTransactionsByLeadId(int leadId);
        List<TransferDto> GetTransfersByLeadId(int leadId);
        List<LeadBalanceDto> GetBalanceByLeadId(int leadId);
    }
}
