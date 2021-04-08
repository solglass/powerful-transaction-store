using System.Collections.Generic;
namespace TransactionStore.API.Utils
{
    public class Quotes
    {
        public static Dictionary<string, decimal> Currency
        {
            get
            {
                return new Dictionary<string, decimal>
                {
                    ["RUB"] = 1, 
                    ["USD"] = 75,               
                    ["EUR"] = 90,                 
                    ["JPY"] = (decimal)0.7 ,
                };
            }
        }
        
    }
}
