using System.ComponentModel.DataAnnotations;
namespace TransactionStore.API.Models.InputModels
{
    public class SimpleTransactionInputModel
    {
        [Required]
        public AccountModel Account { get; set; }
        [Required]
        public ValueModel Value { get; set; }
    }
}
