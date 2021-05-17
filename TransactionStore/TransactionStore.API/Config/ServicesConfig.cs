using Microsoft.Extensions.DependencyInjection;
using TransactionStore.Business;
using TransactionStore.Data;

namespace TransactionStore.API.Config
{
    public static class ServicesConfig
    {
        public static void RegistrateServicesConfig(this IServiceCollection services)
        {
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddSingleton<ConverterService>();
            services.AddSingleton<CurrencyRatesService>();
        }
    }
}