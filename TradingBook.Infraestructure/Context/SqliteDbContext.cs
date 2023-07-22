using Microsoft.EntityFrameworkCore;
using TradingBook.Infraestructure.Seeder;
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
            modelBuilder.ConfigureDepositModel();

            base.OnModelCreating(modelBuilder);

            modelBuilder.AddSeeders();
        }

        public DbSet<CurrencyEntity> Currency { get; set; }

        public DbSet<StockReferenceEntity> StockReference { get; set; }

        public DbSet<StockEntity> Stock { get; set; }

        public DbSet<DepositEntity> Deposit { get; set; }
    }
}
