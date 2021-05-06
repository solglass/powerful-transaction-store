
namespace TransactionStore.API.Models.OutputModels
{
    public class AccountBalanceOutputModel
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
