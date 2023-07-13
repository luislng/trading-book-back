using Microsoft.Extensions.Configuration;
using TradingBook.ExternalServices.Http.Abstract;
using TradingBook.ExternalServices.StockProvider.Abstract;
using TradingBook.ExternalServices.StockProvider.MarketStackProvider.Model;

namespace TradingBook.ExternalServices.StockProvider.MarketStackProvider.Implementation
{
    internal class MarketStackService : IStockProviderService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;

        public MarketStackService(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _configuration = configuration;
        }

        public async Task<decimal> StockPrice(string stockCode)
        {
            Uri uri = BuildUri();
            Dictionary<string, string> parameters = BuildParameters(stockCode);

            MarketStackRoot result = await _httpService.Get<MarketStackRoot>(uri,parameters);

            decimal stockPrice = (decimal)result.Data?.FirstOrDefault()?.Last;

            return stockPrice;
        }

        private Dictionary<string, string> BuildParameters(string stockCode)
        {
            Dictionary<string, string> paramsDictionary = new Dictionary<string, string>();

            paramsDictionary.Add(GetConfigurationValue(MarketStackConfigurationKeys.KEY_PARAM), GetConfigurationValue(MarketStackConfigurationKeys.KEY_VALUE));
            paramsDictionary.Add(GetConfigurationValue(MarketStackConfigurationKeys.INTERVAL_PARAM), GetConfigurationValue(MarketStackConfigurationKeys.INTERVAL_VALUE));
            paramsDictionary.Add(GetConfigurationValue(MarketStackConfigurationKeys.SYMBOL_PARAM), stockCode);

            return paramsDictionary;
        }

        private Uri BuildUri()
        {
            string uriValue = GetConfigurationValue(MarketStackConfigurationKeys.URI);

            Uri uri = new Uri(uriValue);

            return uri;
        }

        private string GetConfigurationValue(string key) => _configuration[key] ?? throw new KeyNotFoundException(key);
    }
}
