using TradingBook.Model.CryptoCurrency;

namespace TradingBook.Application.Services.CryptoCurrencyReference.Abstract
{
    public interface ICryptoCurrencyReferenceService
    {
        public Task<CryptoCurrencyReferenceDto> SaveAsync(CryptoCurrencyReferenceDto cryptoRefDto);

        public Task<List<CryptoCurrencyReferenceDto>> GetAllAsync();

        public Task DeleteAsync(uint id);

        public Task<bool> CheckIfCryptoRefExist(string cryptoCode);
    }
}
