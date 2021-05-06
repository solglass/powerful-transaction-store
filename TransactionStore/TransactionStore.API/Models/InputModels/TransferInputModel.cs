using System.ComponentModel.DataAnnotations;
namespace TransactionStore.API.Models.InputModels
{
    public class TransferInputModel
    {
        [Required]
        public AccountModel SenderAccount { get; set; }
        [Required]
        public AccountModel RecipientAccount { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}