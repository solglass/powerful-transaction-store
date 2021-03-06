using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TransactionStore.API.Config;
using TransactionStore.Core.Settings;

using TransactionStore.API.Middleware;
using MassTransit;

namespace TransactionStore.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json");
            if (!env.IsProduction())
                {
                    builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json");
                }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<AppSettings>(Configuration);
            services.RegistrateServicesConfig();
            services.AddAutoMapper(typeof(Startup));
            services.SwaggerExtention();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CurrencyRatesConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("event-listener", e =>
                    {
                        e.ConfigureConsumer<CurrencyRatesConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "TransactionStore");
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            //if (env.IsProduction())
            //{
            //    app.UseMiddleware<IPAccessMiddleware>();
            //}
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
