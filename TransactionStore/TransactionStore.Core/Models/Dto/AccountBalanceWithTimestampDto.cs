using System;
using System.Collections.Generic;
using System.Text;
using TransactionStore.Core.Enums;

namespace TransactionStore.Core.Models
{
    public class AccountBalanceWithTimestampDto 
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public Currency? Currency { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
