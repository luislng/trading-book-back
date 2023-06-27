using Microsoft.Extensions.DependencyInjection;
using TradingBook.Application.Services.Abstract;
using TradingBook.Application.Services.Implementation;

namespace TradingBook.Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddScoped<IAssetService, AssetService>();

            return services;
        }
    }
}
