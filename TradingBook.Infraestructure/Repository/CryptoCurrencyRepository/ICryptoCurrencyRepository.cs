using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository.CryptoCurrencyRepository
{
    public interface ICryptoCurrencyRepository : IEntityRepository<CryptoCurrencyEntity>
    {
        public Task<CryptoCurrencyEntity> GetById(uint id);   

        public Task<List<CryptoCurrencyEntity>> GetCryptoSelled();
    }
}
