using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Models
{
    public class Value
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
