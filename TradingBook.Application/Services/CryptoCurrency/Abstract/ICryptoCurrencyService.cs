using TradingBook.Model.CryptoCurrency;
using TradingBook.Model.Invest;

namespace TradingBook.Application.Services.CryptoCurrency.Abstract
{
    public interface ICryptoCurrencyService
    {
        public Task<List<CryptoDto>> GetAllAsync();

        public Task<CryptoDto> GetByIdAsync(uint id);

        public Task<uint> SaveCryptoCurrencyAsync(SaveCryptoDto invest);

        public Task<uint> UpdateMarketLimitAsync(MarketLimitsDto marketLimits);

        public Task<uint> SellAsync(SellCryptoDto sellCrypto);

        public Task DeleteAsync(uint id);

        public Task<decimal> TotalEurEarnedAsync();
    }
}
