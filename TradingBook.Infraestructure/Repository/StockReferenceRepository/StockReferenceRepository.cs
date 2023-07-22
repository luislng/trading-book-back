using Microsoft.EntityFrameworkCore;
using TradingBook.Infraestructure.Context;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository.StockReferenceRepository
{
    internal class StockReferenceRepository : IStockReferenceRepository
    {
        private readonly SqliteDbContext _context;

        private StockReferenceRepository() { }   

        public StockReferenceRepository(SqliteDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(SqliteDbContext));                
        }

        public async Task AddAsync(StockReferenceEntity entity)
        {
            await _context.StockReference.AddAsync(entity);
        }

        public async Task RemoveAsync(uint id)
        {
            await _context.StockReference.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<StockReferenceEntity>> GetAllAsync()
        {
            return await _context.StockReference.AsNoTracking().ToListAsync();
        }
    }
}
