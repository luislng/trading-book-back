using Microsoft.EntityFrameworkCore;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Seeder
{
    internal static class DataSeeder
    {
        public static void AddSeeders(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyEntity>().HasData(new CurrencyEntity() { Id = 1, Name = "Euro", Code = "EUR" });
            modelBuilder.Entity<CurrencyEntity>().HasData(new CurrencyEntity() { Id = 2, Name = "Dollar", Code = "USD" });

            modelBuilder.Entity<StockReferenceEntity>().HasData(new StockReferenceEntity() { Id = 1, Name = "Microsoft", Code = "MSFT" });
            modelBuilder.Entity<StockReferenceEntity>().HasData(new StockReferenceEntity() { Id = 2, Name = "TaiwanSemiCond", Code = "TSM" });
        }
    }
}
