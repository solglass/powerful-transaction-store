using System;
namespace TransactionStore.API.Models.OutputModels
{
    public abstract class BaseTransactionOutputModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
