using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository
{
    public interface IEntityRepository<TEntity>: IRepositoryBase where TEntity : EntityBase
    {
        public Task AddAsync(TEntity entity);
        public Task RemoveAsync(uint id);                
        public Task<List<TEntity>> GetAllAsync();
    }
}
