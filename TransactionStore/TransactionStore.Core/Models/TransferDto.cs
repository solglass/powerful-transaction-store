using EducationSystem.Core.Enums;

namespace TransactionStore.Core.Models
{
    public class TransferDto : BaseTransactionDto
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public decimal SenderAmount { get; set; }
        public decimal RecipientAmount { get; set; }
        public Currency SenderCurrency { get; set; }
        public Currency RecipientCurrency { get; set; }
        public override TransactionType Type { get; set; } = TransactionType.Transfer;
    }
}
