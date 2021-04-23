using System;
using System.ComponentModel.DataAnnotations;
namespace TransactionStore.API.Models.InputModels
{
    public class SimpleTransactionInputModel : BaseTransactionInputModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int AccountId { get; set; }
    }
}
