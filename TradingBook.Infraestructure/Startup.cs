using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradingBook.Infraestructure.Context;
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
                        
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IUnitOfWork, SqliteUnitOfWork>();

            services.AddScoped<ICurrencyRepository, CurrencyRepository>();            
            services.AddScoped<IStockReferenceRepository, StockReferenceRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IDepositRepository, DepositRepository>();

            return services;
        }
    }
}
