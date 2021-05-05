namespace TransactionStore.API.Models.OutputModels
{
    public class TransferOutputModel : BaseTransactionOutputModel
    {
        public Account Sender { get; set; }
        public Account Recipient { get; set; }
        public decimal Amoint { get; set; }

    }
}
