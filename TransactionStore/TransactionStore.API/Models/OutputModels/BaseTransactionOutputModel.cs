using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Models.OutputModels
{
    public abstract class BaseTransactionOutputModel
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
