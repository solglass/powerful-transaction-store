﻿using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.API.Attributes;

namespace TransactionStore.API.Models.InputModels
{
    public class SimpleTransactionInputModel : BaseTransactionInputModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int LeadId { get; set; }
    }
}