using Microsoft.Extensions.DependencyInjection;
using TradingBook.ExternalServices.ExchangeProvider.Abstract;
using TradingBook.ExternalServices.ExchangeProvider.ApiExchange.Implementation;
using TradingBook.ExternalServices.Http.Abstract;
using TradingBook.ExternalServices.Http.Implementation;

namespace TradingBook.ExternalServices
{
    public static class Startup
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<ICurrencyExchangeService, CurrencyApiExchangeService>();         

            return services;
        }
    }
}
