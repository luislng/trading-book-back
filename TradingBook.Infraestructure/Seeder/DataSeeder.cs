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
            modelBuilder.Entity<CurrencyEntity>().HasData(new CurrencyEntity() { Id = 3, Name = "BTC", Code = "BTC" });
            modelBuilder.Entity<CurrencyEntity>().HasData(new CurrencyEntity() { Id = 4, Name = "SUI", Code = "SUI" });
            modelBuilder.Entity<CurrencyEntity>().HasData(new CurrencyEntity() { Id = 5, Name = "GTC", Code = "GTC" });

            modelBuilder.Entity<StockReferenceEntity>().HasData(new StockReferenceEntity() { Id = 1, Name = "Microsoft", Code = "MSFT" });
            modelBuilder.Entity<StockReferenceEntity>().HasData(new StockReferenceEntity() { Id = 2, Name = "TaiwanSemiCond", Code = "TSM" });

            modelBuilder.Entity<CryptoCurrencyReferenceEntity>().HasData(new CryptoCurrencyReferenceEntity() { Id = 1, Name = "BTC-EUR", Code = "BTCEUR" });
            modelBuilder.Entity<CryptoCurrencyReferenceEntity>().HasData(new CryptoCurrencyReferenceEntity() { Id = 2, Name = "SUI-EUR", Code = "SUIEUR" });
        }
    }
}
