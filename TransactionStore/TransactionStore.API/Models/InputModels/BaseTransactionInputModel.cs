using System.ComponentModel.DataAnnotations;

namespace TransactionStore.API.Models.InputModels
{
    public abstract class BaseTransactionInputModel
    {
        [Required]
        public ValueModel Value { get; set; }
    }
}
