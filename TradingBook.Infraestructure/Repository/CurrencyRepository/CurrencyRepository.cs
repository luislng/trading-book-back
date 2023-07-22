using Microsoft.EntityFrameworkCore;
using TradingBook.Infraestructure.Context;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository.CurrencyRepository
{
    internal class CurrencyRepository : ICurrencyRepository
    {
        private readonly SqliteDbContext _context;

        public CurrencyRepository(SqliteDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(SqliteDbContext));
        }

        public async Task AddAsync(CurrencyEntity entity)
        {
           await _context.Currency.AddAsync(entity);   
        }
     
        public async Task<List<CurrencyEntity>> GetAllAsync()
        {
            return await _context.Currency.AsNoTracking().ToListAsync();
        }

        public async Task RemoveAsync(uint id)
        {
            await _context.Currency.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
