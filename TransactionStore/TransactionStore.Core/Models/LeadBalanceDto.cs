using EducationSystem.Core.Enums;
using System;

namespace TransactionStore.Core.Models
{
    public class LeadBalanceDto
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
