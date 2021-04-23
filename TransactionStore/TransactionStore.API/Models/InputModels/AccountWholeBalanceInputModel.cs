using System.Collections.Generic;

namespace TransactionStore.API.Models.InputModels
{
    public class AccountWholeBalanceInputModel
    {
        public List<int> Ids { get; set; }
        public string Currency { get; set; }
    }
}
