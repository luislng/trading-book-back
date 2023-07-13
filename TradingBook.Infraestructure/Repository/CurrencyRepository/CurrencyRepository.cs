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

        public void Add(CurrencyEntity entity)
        {
           _context.Currency.Add(entity);   
        }

        public CurrencyEntity Find(uint id)
        {
            throw new NotImplementedException();
        }

        public List<CurrencyEntity> GetAll()
        {
            return _context.Currency.AsNoTracking().ToList();
        }

        public void Remove(uint id)
        {
            CurrencyEntity currencyToRemove = _context.Currency
                                                .Where(x => x.Id == id)
                                                .FirstOrDefault() ?? throw new KeyNotFoundException(nameof(CurrencyEntity));

            _context.Currency.Remove(currencyToRemove); 
        }
    }
}
