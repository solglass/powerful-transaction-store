using System;
namespace TransactionStore.API.Models.OutputModels
{
    public abstract class BaseTransactionOutputModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
