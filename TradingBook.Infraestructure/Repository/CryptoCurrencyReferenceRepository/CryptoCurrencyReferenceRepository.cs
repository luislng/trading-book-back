using Microsoft.EntityFrameworkCore;
using TradingBook.Infraestructure.Context;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository.CryptoCurrencyReferenceRepository
{
    internal class CryptoCurrencyReferenceRepository : ICryptoCurrencyReferenceRepository
    {
        private readonly SqliteDbContext _context;

        public CryptoCurrencyReferenceRepository(SqliteDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(SqliteDbContext));
        }

        public async Task AddAsync(CryptoCurrencyReferenceEntity entity)
        {
            await _context.CryptoCurrencyReference.AddAsync(entity);
        }

        public async Task<List<CryptoCurrencyReferenceEntity>> GetAllAsync()
        {
            return await _context.CryptoCurrencyReference.AsNoTracking().ToListAsync();
        }

        public async Task RemoveAsync(uint id)
        {
            await _context.CryptoCurrencyReference.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
