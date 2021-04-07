using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.API.Attributes;

namespace TransactionStore.API.Models.InputModels
{
    public class BaseTransactionInputModel
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [CustomCurrencyPairValidation]
        public string CurrencyPair { get; set; }
    }
}
