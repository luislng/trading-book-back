using Microsoft.EntityFrameworkCore;
using TradingBook.Infraestructure.Context;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository.StockRepository
{
    internal class StockRepository : IStockRepository
    {
        private readonly SqliteDbContext _context;

        public StockRepository(SqliteDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(SqliteDbContext)); 
        }

        public void Add(StockEntity entity)
        {
           _context.Stock.Add(entity); 
        }

        public StockEntity Find(uint id, bool trackEntity = true)
        {
            IQueryable<StockEntity> stockForUpdate = _context.Stock.Where(x => x.Id == id);

            if (!trackEntity)
                stockForUpdate = stockForUpdate.AsNoTracking();

            stockForUpdate = stockForUpdate.Include(x => x.ReturnStockPrice)
                                           .Include(x => x.Currency);

            StockEntity stockEntity = stockForUpdate.FirstOrDefault() ?? throw new KeyNotFoundException(nameof(StockEntity));

            return stockEntity;
        }

        public List<StockEntity> GetAll()
        {
            return _context.Stock
                           .AsNoTracking()
                           .Include(x=>x.StockReference)
                           .Include(x=>x.Currency)
                           .ToList();
        }

        public void Remove(uint id)
        {
            _context.Stock.Where(x => x.Id == id)
                          .ExecuteDelete();
        }
    }
}
