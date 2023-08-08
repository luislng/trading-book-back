using Microsoft.Extensions.DependencyInjection;
using TradingBook.ExternalServices.CryptoExchange;
using TradingBook.ExternalServices.CryptoExchange.Abstract;
using TradingBook.ExternalServices.CryptoExchange.Binance.Implementation;
using TradingBook.ExternalServices.ExchangeProvider;
using TradingBook.ExternalServices.ExchangeProvider.Abstract;
using TradingBook.ExternalServices.ExchangeProvider.AlphaVantage.Implementation;
using TradingBook.ExternalServices.ExchangeProvider.ApiExchange.Implementation;
using TradingBook.ExternalServices.Http.Abstract;
using TradingBook.ExternalServices.Http.Implementation;
using TradingBook.ExternalServices.StockProvider;
using TradingBook.ExternalServices.StockProvider.Abstract;
using TradingBook.ExternalServices.StockProvider.FinnHubProvider.Implementation;
using TradingBook.ExternalServices.StockProvider.MarketStackProvider.Implementation;

namespace TradingBook.ExternalServices
{
    public static class Startup
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpService, HttpService>();
            services.AddMemoryCache();

            services.AddTransient<ICurrencyExchangeServiceProvider, CurrencyApiExchangeService>();
            services.AddTransient<ICurrencyExchangeServiceProvider, AlphaVantageExchangeService>();
            services.AddTransient<ICurrencyExchangeServiceManager, ExchangeServiceManagerProvider>();

            services.AddTransient<IStockServiceProvider, MarketStackService>();
            services.AddTransient<IStockServiceProvider, FinHubProviderService>();
            services.AddTransient<IStockServiceManager, StockServiceManagerProvider>();

            services.AddTransient<ICryptoExchangeService, BinanceApiCryptoSpotPriceService>();
            services.AddTransient<ICryptoExchangeServiceManager, CryptoExchangeServiceManagerProvider>();

            return services;
        }
    }
}
