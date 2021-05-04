using System.Collections.Generic;

namespace TransactionStore.API.Models.OutputModels
{
    public class WholeBalanceOutputModel
    {
        public List<AccountBalanceOutputModel> Accounts { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
