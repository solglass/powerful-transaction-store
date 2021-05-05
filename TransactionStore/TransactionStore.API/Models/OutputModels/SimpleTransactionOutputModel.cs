namespace TransactionStore.API.Models.OutputModels
{
    public class SimpleTransactionOutputModel : BaseTransactionOutputModel
    {
        public int AccountId { get; set; }
        public Value Value { get; set; }
    }
}
