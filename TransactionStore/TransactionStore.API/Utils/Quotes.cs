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
                    ["USDUSD"] = 1, 
                    ["RUBUSD"] = (decimal)0.0133333,               
                    ["EURUSD"] = (decimal)1.2,                 
                    ["JPYUSD"] = (decimal)0.7 ,
                };
            }
        }
        
    }
}
