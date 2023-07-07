using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TradingBook.ExternalServices.ExchangeProvider.Abstract;
using TradingBook.ExternalServices.ExchangeProvider.ApiExchange.Model;
using TradingBook.ExternalServices.Http.Abstract;

namespace TradingBook.ExternalServices.ExchangeProvider.ApiExchange.Implementation
{
    internal class CurrencyApiExchangeService : ICurrencyExchangeService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;

        public CurrencyApiExchangeService(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(IHttpService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
        }

        public async Task<decimal> ExchangeRate(string currencyCodeFrom, string currencyCodeTo)
        {
            Uri uri = BuildUri();
            Dictionary<string, string> headers = BuildHeaders();
            Dictionary<string, string> parameters = BuildParameters(currencyCodeFrom, currencyCodeTo);

            CurrencyApiExchangeDto result = await _httpService.Get<CurrencyApiExchangeDto>(uri, parameters, headers);

            decimal valueRate = ExtractValueRateFromResult(result);

            return valueRate;
        }

        private static decimal ExtractValueRateFromResult(CurrencyApiExchangeDto result)
        {
            string resultJson = result.Data.ToString();

            DataCurrencyRoot dataCurrencyRoot = JsonSerializer.Deserialize<DataCurrencyRoot>(resultJson);

            string dataCurrencyJson = dataCurrencyRoot.DataCurrency.First()
                                                                   .Value
                                                                   .ToString();

            DataCurrency dataCurrency = JsonSerializer.Deserialize<DataCurrency>(dataCurrencyJson);

            return (decimal)dataCurrency.Value;
        }

        private Dictionary<string, string> BuildParameters(string fromCurrency, string toCurrency)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            string fromCurrencyHeader = GetConfigurationValue(ConfigurationKeysApiExchange.PARAM_FROM_CURRENCY);
            string toCurrencyHeader = GetConfigurationValue(ConfigurationKeysApiExchange.PARAM_TO_CURRENCY);

            parameters.Add(fromCurrencyHeader, fromCurrency);
            parameters.Add(toCurrencyHeader, toCurrency);

            return parameters;
        }

        private Dictionary<string, string> BuildHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            string apiKeyValue = GetConfigurationValue(ConfigurationKeysApiExchange.API_KEY_VALUE);
            string headerApiKey = GetConfigurationValue(ConfigurationKeysApiExchange.API_KEY_HEADER);

            headers.Add(headerApiKey, apiKeyValue);

            return headers;
        }

        private Uri BuildUri()
        {
            string uriValue = GetConfigurationValue(ConfigurationKeysApiExchange.URI);

            Uri uri = new Uri(uriValue);

            return uri;
        }

        private string GetConfigurationValue(string key) => _configuration[key] ?? throw new KeyNotFoundException(key);
    }
}
