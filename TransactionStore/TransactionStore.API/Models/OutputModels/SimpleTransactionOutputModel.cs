namespace TransactionStore.API.Models.OutputModels
{
    public class SimpleTransactionOutputModel : BaseTransactionOutputModel
    {
        public int AccountId { get; set; }
        public ValueModel Value { get; set; }
    }
}
