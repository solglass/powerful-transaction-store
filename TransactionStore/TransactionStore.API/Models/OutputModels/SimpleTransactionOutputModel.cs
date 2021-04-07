using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Models.OutputModels
{
    public class SimpleTransactionOutputModel : BaseTransactionOutputModel
    {
        public int LeadId { get; set; }
    }
}
