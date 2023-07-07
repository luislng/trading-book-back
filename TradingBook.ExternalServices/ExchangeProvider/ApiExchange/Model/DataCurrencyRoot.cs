using System.Text.Json.Serialization;

namespace TradingBook.ExternalServices.ExchangeProvider.ApiExchange.Model
{
    public class DataCurrencyRoot
    {
        [JsonExtensionData]
        public IDictionary<string,object> DataCurrency { get; set; }
    }
}
