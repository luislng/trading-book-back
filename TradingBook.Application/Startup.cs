using Microsoft.Extensions.DependencyInjection;
using TradingBook.Application.Services.AssetService.Abstract;
using TradingBook.Application.Services.AssetService.Implementation;
using TradingBook.Application.Services.CurrencyService.Abstract;
using TradingBook.Application.Services.CurrencyService.Implementation;
using TradingBook.ExternalServices;

namespace TradingBook.Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddExternalServices();

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<ICurrencyService, CurrencyService>();

            return services;
        }
    }
}
