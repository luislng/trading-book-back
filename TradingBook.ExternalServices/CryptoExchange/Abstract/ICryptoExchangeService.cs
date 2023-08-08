using TradingBook.Model.CryptoCurrency;

namespace TradingBook.ExternalServices.CryptoExchange.Abstract
{
    internal interface ICryptoExchangeService
    {
        public Task<CryptoExchangeSpotPrice> SpotPrice(string cryptoCode);
    }
}