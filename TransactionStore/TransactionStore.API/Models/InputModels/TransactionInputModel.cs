using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Models.InputModels
{
    public class TransactionInputModel
    {
        public int LeadId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency {get; set; }
        public int Type { get; set; }
        public DateTime Timestamp { get; set; }


    }
}
