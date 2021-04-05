﻿using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.API.Attributes;

namespace TransactionStore.API.Models.InputModels
{
    public class TransactionInputModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int LeadId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public Currency Currency {get; set; }
        
        [Required]
        [Range(1, 2)]
        //ToDO MUST BE GONE 
        public TransactionType Type { get; set; }


    }
}
