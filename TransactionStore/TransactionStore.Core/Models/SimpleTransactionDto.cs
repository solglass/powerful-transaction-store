using TransactionStore.Core.Enums;
using System;

namespace TransactionStore.Core.Models
{
    public class SimpleTransactionDto : BaseTransactionDto
    {
        public int AccountId { get; set; }
    }
}
