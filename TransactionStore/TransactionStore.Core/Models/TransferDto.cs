using System;
using TransactionStore.Core.Models;

namespace TransactionStore.Core.Models
{
    public class TransferDto : TransactionDto
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
    }
}
