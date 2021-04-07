using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionStore.Core.Models
{
    public abstract class BaseTransactionDto
    {
        public int? Id { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public TransactionType Type { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
