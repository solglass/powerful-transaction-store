using TransactionStore.Core.Enums;

namespace TransactionStore.Core.Models
{
    public class TransferDto : BaseTransactionDto
    {
        public TransferDto()
        {
            Type = TransactionType.Transfer;
        }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public decimal SenderAmount { get; set; }
        public decimal RecipientAmount { get; set; }
        public Currency SenderCurrency { get; set; }
        public Currency RecipientCurrency { get; set; }
    }
}
