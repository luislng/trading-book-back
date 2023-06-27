using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Context
{
    internal class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureAssetModel();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Asset> Assets { get; set; }
    }
}
