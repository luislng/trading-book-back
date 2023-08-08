using Microsoft.EntityFrameworkCore;
using TradingBook.Infraestructure.Context;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository.CryptoCurrencyRepository
{
    internal class CryptoCurrencyRepository : ICryptoCurrencyRepository
    {
        private readonly SqliteDbContext _context;

        public CryptoCurrencyRepository(SqliteDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(SqliteDbContext));
        }

        public async Task AddAsync(CryptoCurrencyEntity entity)
        {
            await _context.CryptoCurrency.AddAsync(entity);
        }

        public async Task<List<CryptoCurrencyEntity>> GetAllAsync()
        {
            return await _context.CryptoCurrency.AsNoTracking()
                                                .Include(x => x.CryptoCurrencyReferenceFrom)
                                                .Include(x => x.CryptoCurrencyReferenceTo)
                                                .ToListAsync();
        }

        public async Task<CryptoCurrencyEntity> GetById(uint id)
        {
            return await _context.CryptoCurrency
                                 .Include(x=>x.CryptoCurrencyReferenceFrom)
                                 .Include(x => x.CryptoCurrencyReferenceTo)
                                 .Where(x => x.Id == id)
                                 .FirstOrDefaultAsync();
        }

        public async Task<List<CryptoCurrencyEntity>> GetCryptoSelled()
        {
            return await _context.CryptoCurrency.Where(x => x.IsSelled)
                                   .AsNoTracking()
                                   .Include(x => x.CryptoCurrencyReferenceFrom)
                                   .Include(x => x.CryptoCurrencyReferenceTo)
                                   .ToListAsync();
        }

        public async Task RemoveAsync(uint id)
        {
            await _context.CryptoCurrency.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
