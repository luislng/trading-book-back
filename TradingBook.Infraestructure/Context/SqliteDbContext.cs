using Microsoft.EntityFrameworkCore;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Context
{
    internal class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureAssetModel();
            modelBuilder.ConfigureCurrencyModel();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Asset> Asset { get; set; }

        public DbSet<Currency> Currency { get; set; }
    }
}
