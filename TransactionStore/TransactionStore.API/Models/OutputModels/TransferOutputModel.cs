using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Models.OutputModels
{
    public class TransferOutputModel : TransactionOutputModel
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
    }
}
