﻿using Microsoft.EntityFrameworkCore;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Context
{
    internal static class ConfigurationModel
    {
        private const int MAX_LENGTH_ASSET_STRING = 50;

        public static ModelBuilder ConfigureAssetModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                       .Property(x => x.Name)
                       .HasMaxLength(MAX_LENGTH_ASSET_STRING);

            modelBuilder.Entity<Asset>()
                  .Property(x => x.Code)
                  .HasMaxLength(MAX_LENGTH_ASSET_STRING);

            return modelBuilder;
        }
    }
}
