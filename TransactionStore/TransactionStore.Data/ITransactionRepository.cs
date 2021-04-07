﻿using System;
using System.Collections.Generic;
using TransactionStore.Core.Models;

namespace TransactionStore.Data
{
    public interface ITransactionRepository
    {
        int AddDepositeOrWithdraw(SimpleTransactionDto dto);
        int AddTransfer(TransferDto dto);
        List<SimpleTransactionDto> GetDepositOrWithdrawByLeadId(int leadId, int type);
        List<TransferDto> GetTransfersByLeadId(int leadId);
        List<LeadBalanceDto> GetBalanceByLeadId(int leadId);
    }
}
