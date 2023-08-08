using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradingBook.Infraestructure.Context;
using TradingBook.Infraestructure.Repository.CryptoCurrencyReferenceRepository;
using TradingBook.Infraestructure.Repository.CryptoCurrencyRepository;
using TradingBook.Infraestructure.Repository.CurrencyRepository;
using TradingBook.Infraestructure.Repository.DepositRepository;
using TradingBook.Infraestructure.Repository.Factory;
using TradingBook.Infraestructure.Repository.StockReferenceRepository;
using TradingBook.Infraestructure.Repository.StockRepository;
using TradingBook.Infraestructure.UnitOfWork;

namespace TradingBook.Infraestructure
{
    public static class Startup
    {
        private const string CONNECTION_STRING_CONFIGURATION = "SqliteConnection";

        public static IServiceCollection AddInfraestructure(this IServiceCollection services,IConfiguration appConfiguration)
        {
            string connectionString = appConfiguration.GetConnectionString(CONNECTION_STRING_CONFIGURATION) 
                ?? throw new NullReferenceException(nameof(CONNECTION_STRING_CONFIGURATION));

            services.AddDbContext<SqliteDbContext>(opt => opt.UseSqlite(connectionString));
                        
            services.AddTransient<IRepositoryFactory, RepositoryFactory>();
            services.AddTransient<IUnitOfWork, SqliteUnitOfWork>();

            services.AddTransient<ICurrencyRepository, CurrencyRepository>();            
            services.AddTransient<IStockReferenceRepository, StockReferenceRepository>();
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddTransient<IDepositRepository, DepositRepository>();
            services.AddTransient<ICryptoCurrencyRepository, CryptoCurrencyRepository>();
            services.AddTransient<ICryptoCurrencyReferenceRepository, CryptoCurrencyReferenceRepository>();

            return services;
        }
    }
}
