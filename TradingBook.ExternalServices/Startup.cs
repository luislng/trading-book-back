using Microsoft.Extensions.DependencyInjection;
using TradingBook.ExternalServices.ExchangeProvider;
using TradingBook.ExternalServices.ExchangeProvider.Abstract;
using TradingBook.ExternalServices.ExchangeProvider.AlphaVantage.Implementation;
using TradingBook.ExternalServices.ExchangeProvider.ApiExchange.Implementation;
using TradingBook.ExternalServices.Http.Abstract;
using TradingBook.ExternalServices.Http.Implementation;
using TradingBook.ExternalServices.StockProvider.Abstract;
using TradingBook.ExternalServices.StockProvider.MarketStackProvider.Implementation;

namespace TradingBook.ExternalServices
{
    public static class Startup
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpService, HttpService>();
            services.AddMemoryCache();
            services.AddTransient<ICurrencyExchangeService, CurrencyApiExchangeService>();
            services.AddTransient<ICurrencyExchangeService, AlphaVantageExchangeService>();
            services.AddTransient<ICurrencyExchangeServiceManager, ExchangeLoadBalancerProvider>();

            services.AddTransient<IStockProviderService, MarketStackService>();
            return services;
        }
    }
}
