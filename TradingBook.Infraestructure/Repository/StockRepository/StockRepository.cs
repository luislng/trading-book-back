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

        public async Task AddAsync(StockEntity entity)
        {
           await _context.Stock.AddAsync(entity); 
        }

        public async Task<StockEntity> FindAsync(uint id, bool trackEntity = true)
        {
            IQueryable<StockEntity> stockForUpdate = _context.Stock.Where(x => x.Id == id);

            if (!trackEntity)
                stockForUpdate = stockForUpdate.AsNoTracking();

            stockForUpdate = stockForUpdate.Include(x => x.StockReference)
                                           .Include(x => x.Currency);

            StockEntity stockEntity = await stockForUpdate.FirstOrDefaultAsync() ?? throw new KeyNotFoundException(nameof(StockEntity));

            return stockEntity;
        }

        public async Task<List<StockEntity>> GetAllAsync()
        {
            return await _context.Stock
                           .AsNoTracking()
                           .Include(x=>x.StockReference)
                           .Include(x=>x.Currency)
                           .ToListAsync();
        }

        public async Task RemoveAsync(uint id)
        {
            await _context.Stock.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
