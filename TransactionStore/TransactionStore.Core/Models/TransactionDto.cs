using EducationSystem.Core.Enums;
using System;

namespace TransactionStore.Core.Models
{
    public class TransactionDto
    {
        public int? Id { get; set; }
        public int LeadId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public TransactionType Type { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
