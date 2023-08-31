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
                     .HasKey(x => x.Id);

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
                       .HasKey(x => x.Id);

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
                        .HasKey(x => x.Id);

            modelBuilder.Entity<StockEntity>()
                        .HasOne(x => x.StockReference)
                        .WithMany(x => x.Stocks)
                        .HasForeignKey(x => x.StockReferenceId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired(); 

            modelBuilder.Entity<StockEntity>()
                        .HasOne(x => x.Currency)
                        .WithMany(x => x.Stocks)
                        .HasForeignKey(x => x.CurrencyId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            modelBuilder.Entity<StockEntity>()
                        .Property(x=>x.IsSelled)
                        .HasDefaultValue(false);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureDepositModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepositEntity>()
                        .HasKey(x => x.Id);

            modelBuilder.Entity<DepositEntity>()
                        .Property(x => x.Deposit)
                        .IsRequired(true)
                        .HasDefaultValue(0.0M);

            modelBuilder.Entity<DepositEntity>()
                     .Property(x => x.DepositDate)
                     .IsRequired(true);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureCryptoModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CryptoCurrencyEntity>()
                        .HasKey(x => x.Id);

            modelBuilder.Entity<CryptoCurrencyEntity>()
                       .HasOne(x => x.CurrencyFrom)
                       .WithMany(x => x.CryptoCurrenciesFrom)
                       .HasForeignKey(x => x.CurrencyFromId)
                       .OnDelete(DeleteBehavior.Restrict)
                       .IsRequired();

            modelBuilder.Entity<CryptoCurrencyEntity>()
                     .HasOne(x => x.CurrencyTo)
                     .WithMany(x => x.CryptoCurrenciesTo)
                     .HasForeignKey(x => x.CurrencyToId)
                     .OnDelete(DeleteBehavior.Restrict)
                     .IsRequired();

            modelBuilder.Entity<CryptoCurrencyEntity>()
                   .HasOne(x => x.CryptoReference)
                   .WithMany(x => x.CryptoCurrencyReferences)
                   .HasForeignKey(x => x.CryptoReferenceId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            modelBuilder.Entity<CryptoCurrencyEntity>()
                        .Property(x => x.IsSelled)
                        .HasDefaultValue(false);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureCryptoReferenceModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CryptoCurrencyReferenceEntity>()
                        .HasKey(x => x.Id);

            return modelBuilder;
        }
    }
}
