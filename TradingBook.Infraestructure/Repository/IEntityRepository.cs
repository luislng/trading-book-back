using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository
{
    public interface IEntityRepository<TEntity>: IRepositoryBase where TEntity : EntityBase
    {
        public void Add(TEntity entity);
        public void Remove(uint id);                
        public List<TEntity> GetAll();
    }
}
