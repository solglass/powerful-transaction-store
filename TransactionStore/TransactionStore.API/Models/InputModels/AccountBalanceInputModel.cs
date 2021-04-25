using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TransactionStore.API.Attributes;

namespace TransactionStore.API.Models.InputModels
{
    public class AccountBalanceInputModel
    {
        [Required]
        [CustomAccountsValidation]
        public List<int> AccountIds { get; set; }
        [Required]
        [CustomCurrencyValidation]
        public string Currency { get; set; }
    }
}
