using System.Collections.Generic;
using TransactionStore.Core.Enums;

namespace TransactionStore.Core.Models
{
    public class WholeBalanceDto
    {
        public List<AccountBalanceDto> Accounts { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }
    }
}
