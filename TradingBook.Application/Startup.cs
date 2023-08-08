using Microsoft.Extensions.DependencyInjection;
using TradingBook.Application.Services.CryptoCurrency.Abstract;
using TradingBook.Application.Services.CryptoCurrency.Implementation;
using TradingBook.Application.Services.CryptoCurrencyReference.Abstract;
using TradingBook.Application.Services.CryptoCurrencyReference.Implementation;
using TradingBook.Application.Services.Currency.Abstract;
using TradingBook.Application.Services.Currency.Implementation;
using TradingBook.Application.Services.Deposit.Abstract;
using TradingBook.Application.Services.Deposit.Implementation;
using TradingBook.Application.Services.Stock.Abstract;
using TradingBook.Application.Services.Stock.Implementation;
using TradingBook.Application.Services.StockReference.Abstract;
using TradingBook.Application.Services.StockReference.Implementation;
using TradingBook.ExternalServices;

namespace TradingBook.Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddExternalServices();

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddScoped<IStockReferenceService, StockReferenceService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IDepositService, DepositService>();
            services.AddScoped<ICryptoCurrencyService, CryptoCurrencyService>();
            services.AddScoped<ICryptoCurrencyReferenceService, CryptoCurrencyReferenceService>();

            return services;
        }
    }
}
