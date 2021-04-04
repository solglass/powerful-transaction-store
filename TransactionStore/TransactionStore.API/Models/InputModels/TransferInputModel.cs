using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Models.InputModels
{
    public class TransferInputModel : TransactionInputModel
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
    }
}
