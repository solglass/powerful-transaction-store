namespace TransactionStore.API.Models.OutputModels
{
    public class TransferOutputModel : BaseTransactionOutputModel
    {
        public AccountModel Sender { get; set; }
        public AccountModel Recipient { get; set; }
        public decimal Amount { get; set; }

    }
}
