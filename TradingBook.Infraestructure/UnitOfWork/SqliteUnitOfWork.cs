using TradingBook.Infraestructure.Context;
using TradingBook.Infraestructure.Repository;
using TradingBook.Infraestructure.Repository.Factory;

namespace TradingBook.Infraestructure.UnitOfWork
{
    internal class SqliteUnitOfWork : IUnitOfWork   
    {
        private readonly SqliteDbContext _context;
        private readonly IRepositoryFactory _repositoryFactory;

        public SqliteUnitOfWork(SqliteDbContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(SqliteDbContext));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(IRepositoryFactory));
        }

        public T GetRepository<T>() where T : class, IRepositoryBase
        {
            return _repositoryFactory.GetInstance<T>();            
        }

        public void SaveChanges()
        {
            _context.SaveChanges(); 
        }
    }
}
