using TransactionStore.Core.Enums;

namespace TransactionStore.Core.Models
{
    public class TransferDto : BaseTransactionDto
    {
        public TransferDto()
        {
            Type = TransactionType.Transfer;
        }
        public int SenderAccountId { get; set; }
        public int RecipientAccountId { get; set; }
        public decimal SenderAccountAmount { get; set; }
        public decimal RecipientAccountAmount { get; set; }
        public Currency SenderAccountCurrency { get; set; }
        public Currency RecipientAccountCurrency { get; set; }
    }
}
