﻿using System.ComponentModel.DataAnnotations;
using TransactionStore.API.Attributes;

namespace TransactionStore.API.Models.InputModels
{
    public abstract class BaseTransactionInputModel
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [CustomCurrencyPairValidation]
        public string CurrencyPair { get; set; }
    }
}
