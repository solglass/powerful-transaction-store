namespace TransactionStore.API.Models.OutputModels
{
    public class TransferOutputModel : BaseTransactionOutputModel
    {
        public int SenderAccountId { get; set; }
        public int RecipientAccountId { get; set; }
    }
}
