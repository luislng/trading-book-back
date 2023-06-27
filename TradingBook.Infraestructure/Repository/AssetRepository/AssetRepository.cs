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
            _context.Assets.Add(entity);
        }

        public void Find(uint id)
        {
            throw new NotImplementedException();
        }

        public void Remove(uint id)
        {   
            Asset assetForDelete = _context.Assets.FirstOrDefault(x => x.Id == id);
            
            if (assetForDelete == null) 
                throw new KeyNotFoundException(nameof(Asset));
            
            _context.Assets.Remove(assetForDelete);
        }

        public void Update(uint id, Asset entity)
        {
            throw new NotImplementedException();
        }

        public List<Asset> GetAll()
        {
            return _context.Assets.AsNoTracking().ToList();
        }
    }
}
