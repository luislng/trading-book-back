using Microsoft.EntityFrameworkCore;
using TradingBook.Infraestructure.Context;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository.DepositRepository
{
    internal class DepositRepository : IDepositRepository
    {
        private readonly SqliteDbContext _context;

        public DepositRepository(SqliteDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(SqliteDbContext));
        }

        public async Task AddAsync(DepositEntity entity)
        {
            await _context.Deposit.AddAsync(entity);
        }

        public async Task<List<DepositEntity>> GetAllAsync()
        {
            return await _context.Deposit.AsNoTracking().ToListAsync();
        }

        public Task RemoveAsync(uint id)
        {
            throw new NotImplementedException();
        }
    }
}
