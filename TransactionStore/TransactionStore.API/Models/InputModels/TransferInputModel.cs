using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TransactionStore.API.Models.InputModels
{
    public class TransferInputModel
    {
        public Account SenderAccount { get; set; }
        public Account RecipientAccount { get; set; }
        public decimal Amount { get; set; }
    }
}