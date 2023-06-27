using Microsoft.Extensions.DependencyInjection;

namespace TradingBook.Infraestructure.Repository.Factory
{
    internal class RepositoryFactory: IRepositoryFactory
    {
        private readonly IServiceProvider _repositoryProvider;

        public RepositoryFactory(IServiceProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(IServiceProvider)); 
        }

        public TRepository GetInstance<TRepository>() where TRepository : class, IRepositoryBase
        {
            TRepository repository = _repositoryProvider.GetRequiredService<TRepository>();
            return repository;
        }
    }
}
