using TradingBook.Infraestructure.Repository;

namespace TradingBook.Infraestructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        public TRepository GetRepository<TRepository>() where TRepository: class, IRepositoryBase;

        public void SaveChanges();
    }
}
