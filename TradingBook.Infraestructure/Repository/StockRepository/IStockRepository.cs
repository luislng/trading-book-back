using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository.StockRepository
{
    public interface IStockRepository:IEntityRepository<StockEntity>
    {
        public StockEntity Find(uint id, bool trackEntity = true);
    }
}
