using Microsoft.EntityFrameworkCore;
using TradingBook.Infraestructure.Context;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository.AssetRepository
{
    internal class AssetRepository : IAssetRepository
    {
        private readonly SqliteDbContext _context;

        private AssetRepository() { }   

        public AssetRepository(SqliteDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(SqliteDbContext));                
        }

        public void Add(Asset entity)
        {
            _context.Asset.Add(entity);
        }

        public void Find(uint id)
        {
            throw new NotImplementedException();
        }

        public void Remove(uint id)
        {   
            Asset assetForDelete = _context.Asset.Where(x => x.Id == id)
                                                 .FirstOrDefault() ?? throw new KeyNotFoundException(nameof(Asset));
            
            _context.Asset.Remove(assetForDelete);
        }

        public void Update(uint id, Asset entity)
        {
            throw new NotImplementedException();
        }

        public List<Asset> GetAll()
        {
            return _context.Asset.AsNoTracking().ToList();
        }
    }
}
