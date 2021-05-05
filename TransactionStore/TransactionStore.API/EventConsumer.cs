using EventContracts;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.Business;
using Newtonsoft.Json.Linq;

namespace TransactionStore.API
{
    public class EventConsumer : IConsumer<ValueEntered>
    {
        public async Task Consume(ConsumeContext<ValueEntered> context)
        {
            var json = JObject.Parse(context.Message.Value);
            var result = json["quotes"].Select(s => new
           {
               CurrencyName = (s as JProperty).Name,
               CurrencyValue = (s as JProperty).Value
            })
            .ToDictionary(k => k.CurrencyName, v => Convert.ToDecimal(v.CurrencyValue));
            CurrencyRatesService.CurrencyPair = result;
        }
    }
}
