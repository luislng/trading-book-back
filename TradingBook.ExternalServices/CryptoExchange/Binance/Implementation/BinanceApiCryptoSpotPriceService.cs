using Microsoft.Extensions.Configuration;
using TradingBook.ExternalServices.CryptoExchange.Abstract;
using TradingBook.ExternalServices.CryptoExchange.Binance.Model;
using TradingBook.ExternalServices.Http.Abstract;
using TradingBook.Model.CryptoCurrency;

namespace TradingBook.ExternalServices.CryptoExchange.Binance.Implementation
{
    internal class BinanceApiCryptoSpotPriceService : ICryptoExchangeService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;

        public BinanceApiCryptoSpotPriceService(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(IHttpService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
        }

        public async Task<CryptoExchangeSpotPrice> SpotPrice(string cryptoCode)
        {
            Uri uri = BuildUri();

            Dictionary<string, string> parameters = BuildParameters(cryptoCode);

            BinanceRoot result = await _httpService.Get<BinanceRoot>(uri, parameters);

            CryptoExchangeSpotPrice cryptoExchangeSpotPrice = new CryptoExchangeSpotPrice
            {
                CryptoCode = result.Symbol,
                SpotPrice = decimal.Parse(result.Price)

            };

            return cryptoExchangeSpotPrice;
        }

        private Dictionary<string, string> BuildParameters(string cryptoCode)
        {
            Dictionary<string, string> paramsDictionary = new Dictionary<string, string>();

            paramsDictionary.Add(GetConfigurationValue(BinanceConfigurationKeys.PRICE_PARAM), cryptoCode);

            return paramsDictionary;
        }

        private Uri BuildUri()
        {
            string uriValue = GetConfigurationValue(BinanceConfigurationKeys.URI);

            Uri uri = new Uri(uriValue);

            return uri;
        }

        private string GetConfigurationValue(string key) => _configuration[key] ?? throw new KeyNotFoundException(key);
    }
}
