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

        public void Add(StockReferenceEntity entity)
        {
            _context.StockReference.Add(entity);
        }

        public void Remove(uint id)
        {
            StockReferenceEntity assetForDelete = _context.StockReference.Where(x => x.Id == id)
                                                 .FirstOrDefault() ?? throw new KeyNotFoundException(nameof(StockReferenceEntity));
            
            _context.StockReference.Remove(assetForDelete);
        }

        public List<StockReferenceEntity> GetAll()
        {
            return _context.StockReference.AsNoTracking().ToList();
        }
    }
}
