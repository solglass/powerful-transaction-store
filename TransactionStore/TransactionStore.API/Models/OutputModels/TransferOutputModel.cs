namespace TransactionStore.API.Models.OutputModels
{
    public class TransferOutputModel : BaseTransactionOutputModel
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
    }
}
