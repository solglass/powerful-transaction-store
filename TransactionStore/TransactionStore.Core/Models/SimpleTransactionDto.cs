using EducationSystem.Core.Enums;
using System;

namespace TransactionStore.Core.Models
{
    public class SimpleTransactionDto : BaseTransactionDto
    {
        public int LeadId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
