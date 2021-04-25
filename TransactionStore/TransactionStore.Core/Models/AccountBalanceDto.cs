using TransactionStore.Core.Enums;

namespace TransactionStore.Core.Models
{
    public class AccountBalanceDto
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
