using Microsoft.EntityFrameworkCore;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Context
{
    internal static class ConfigurationModel
    {
        private const int MAX_LENGTH_ASSET_STRING = 50;

        public static ModelBuilder ConfigureStockReferenceModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockReferenceEntity>()
                       .Property(x => x.Name)
                       .IsRequired()
                       .HasMaxLength(MAX_LENGTH_ASSET_STRING);

            modelBuilder.Entity<StockReferenceEntity>()
                  .Property(x => x.Code)
                  .IsRequired()
                  .HasMaxLength(MAX_LENGTH_ASSET_STRING);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureCurrencyModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyEntity>()
                       .Property(x => x.Name)
                       .IsRequired()
                       .HasMaxLength(MAX_LENGTH_ASSET_STRING);

            modelBuilder.Entity<CurrencyEntity>()
                  .Property(x => x.Code)
                  .IsRequired()
                  .HasMaxLength(MAX_LENGTH_ASSET_STRING);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureStockModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockEntity>()
                        .HasOne(x => x.StockReference)
                        .WithMany(x => x.Stocks)
                        .HasForeignKey(x => x.StockReferenceId);

            modelBuilder.Entity<StockEntity>()
                                 .HasOne(x => x.Currency)
                                 .WithMany(x => x.Stocks)
                                 .HasForeignKey(x => x.CurrencyId);

            modelBuilder.Entity<StockEntity>()
                        .Property(x=>x.IsSelled)
                        .HasDefaultValue(false);

            return modelBuilder;
        }
    }
}
