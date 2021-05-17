using EventContracts;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.Business;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TransactionStore.API
{
    public class CurrencyRatesConsumer : IConsumer<CurrencyRates>
    {
        private CurrencyRatesService _currencyRatesService;
        public CurrencyRatesConsumer(CurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }
        public async Task Consume(ConsumeContext<CurrencyRates> context)
        {
            _currencyRatesService.CurrencyPair = context.Message.Value;
        }
    }
}
