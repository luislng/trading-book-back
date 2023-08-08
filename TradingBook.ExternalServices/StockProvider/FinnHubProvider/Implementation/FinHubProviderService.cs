using Microsoft.Extensions.Configuration;
using TradingBook.ExternalServices.Http.Abstract;
using TradingBook.ExternalServices.StockProvider.Abstract;
using TradingBook.ExternalServices.StockProvider.FinnHubProvider.Model;

namespace TradingBook.ExternalServices.StockProvider.FinnHubProvider.Implementation
{
    internal class FinHubProviderService : IStockServiceProvider
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;

        public FinHubProviderService(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(IHttpService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration)); 
        }

        public async Task<decimal> RequestStockPrice(string stockCode)
        {
            Uri uri = BuildUri();

            Dictionary<string, string> parameters = BuildParameters(stockCode);

            FinHubRoot result = await _httpService.Get<FinHubRoot>(uri, parameters);

            decimal stockPrice = (decimal)result.CurrenStockPrice;

            return stockPrice;
        }

        private Dictionary<string, string> BuildParameters(string stockCode)
        {
            Dictionary<string, string> paramsDictionary = new Dictionary<string, string>();

            paramsDictionary.Add(GetConfigurationValue(FinHubConfigurationKey.KEY_PARAM), GetConfigurationValue(FinHubConfigurationKey.KEY_VALUE));            
            paramsDictionary.Add(GetConfigurationValue(FinHubConfigurationKey.SYMBOL_PARAM), stockCode);

            return paramsDictionary;
        }

        private Uri BuildUri()
        {
            string uriValue = GetConfigurationValue(FinHubConfigurationKey.URI);

            Uri uri = new Uri(uriValue);

            return uri;
        }

        private string GetConfigurationValue(string key) => _configuration[key] ?? throw new KeyNotFoundException(key);
    }
}
