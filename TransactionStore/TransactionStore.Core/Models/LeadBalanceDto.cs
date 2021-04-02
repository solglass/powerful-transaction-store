using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionStore.Data.Models
{
    public class LeadBalanceDto
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
