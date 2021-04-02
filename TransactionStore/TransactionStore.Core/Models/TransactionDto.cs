using System;

namespace TransactionStore.Core.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public decimal Amount { get; set; }
        public int Type { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
