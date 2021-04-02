using System;

namespace TransactionStore.Data.Models
{
    public class TransferDto : TransactionDto
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
    }
}
