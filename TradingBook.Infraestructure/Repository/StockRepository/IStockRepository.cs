using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository.StockRepository
{
    public interface IStockRepository:IEntityRepository<StockEntity>
    {
        public Task<StockEntity> FindAsync(uint id, bool trackEntity = true);
    }
}
