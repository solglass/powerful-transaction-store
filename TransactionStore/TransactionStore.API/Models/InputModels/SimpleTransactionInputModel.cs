using System.ComponentModel.DataAnnotations;
namespace TransactionStore.API.Models.InputModels
{
    public class SimpleTransactionInputModel : BaseTransactionInputModel
    {
        [Required]
        public AccountModel Account { get; set; }
    }
}
