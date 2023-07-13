using Microsoft.EntityFrameworkCore;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Context
{
    internal class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureStockReferenceModel();
            modelBuilder.ConfigureCurrencyModel();
            modelBuilder.ConfigureStockModel();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CurrencyEntity> Currency { get; set; }

        public DbSet<StockReferenceEntity> StockReference { get; set; }

        public DbSet<StockEntity> Stock { get; set; }
    }
}
