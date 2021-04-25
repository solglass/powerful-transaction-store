using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TransactionStore.API.Models.InputModels
{
    public class TransferInputModel : BaseTransactionInputModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int SenderAccountId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int RecipientAccountId { get; set; }
    }
}
