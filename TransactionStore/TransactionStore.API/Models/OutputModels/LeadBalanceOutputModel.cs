using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Models.OutputModels
{
    public class LeadBalanceOutputModel
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
