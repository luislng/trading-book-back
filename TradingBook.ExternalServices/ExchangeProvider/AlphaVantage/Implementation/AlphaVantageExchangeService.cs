using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TradingBook.ExternalServices.ExchangeProvider.Abstract;
using TradingBook.ExternalServices.ExchangeProvider.AlphaVantage.Model;
using TradingBook.ExternalServices.Http.Abstract;

namespace TradingBook.ExternalServices.ExchangeProvider.AlphaVantage.Implementation
{
    internal class AlphaVantageExchangeService : ICurrencyExchangeServiceProvider
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;

        public AlphaVantageExchangeService(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(IHttpService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
        }

        public async Task<decimal> ExchangeRate(string currencyCodeFrom, string currencyCodeTo)
        {
            Uri uri = BuildUri();            
            Dictionary<string, string> parameters = BuildParameters(currencyCodeFrom, currencyCodeTo);

            AlphaVantageRoot result = await _httpService.Get<AlphaVantageRoot>(uri, parameters);

            string jsonResult = result.Values.First().Value.ToString();

            CurrencyValue currencyValue = JsonSerializer.Deserialize<CurrencyValue>(jsonResult);

            decimal currencyExchange = Decimal.Parse(currencyValue.ExchangeRate);

            return currencyExchange;
        }

        private Dictionary<string, string> BuildParameters(string fromCurrency, string toCurrency)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add(ConfigurationKeyAlphaVantage.FUNCTION_KEY, ConfigurationKeyAlphaVantage.FUNCTION_VALUE);

            string fromCurrencyHeader = GetConfigurationValue(ConfigurationKeyAlphaVantage.PARAM_FROM_CURRENCY);
            parameters.Add(fromCurrencyHeader, fromCurrency);

            string toCurrencyHeader = GetConfigurationValue(ConfigurationKeyAlphaVantage.PARAM_TO_CURRENCY);
            parameters.Add(toCurrencyHeader, toCurrency);

            string apiKeyValue = GetConfigurationValue(ConfigurationKeyAlphaVantage.API_KEY_VALUE);
            string headerApiKey = GetConfigurationValue(ConfigurationKeyAlphaVantage.API_KEY_HEADER);

            parameters.Add(headerApiKey, apiKeyValue);

            return parameters;
        }

        private Uri BuildUri()
        {
            string uriValue = GetConfigurationValue(ConfigurationKeyAlphaVantage.URI);

            Uri uri = new Uri(uriValue);

            return uri;
        }

        private string GetConfigurationValue(string key) => _configuration[key] ?? throw new KeyNotFoundException(key);
    }
}
