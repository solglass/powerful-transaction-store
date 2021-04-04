using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Models.InputModels
{
    public class TransferOutputModel : TransactionOutputModel
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public override int LeadId 
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
