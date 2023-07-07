using System.Text.Json.Serialization;

namespace TradingBook.ExternalServices.ExchangeProvider.ApiExchange.Model
{
    public class DataCurrency
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = String.Empty;

        [JsonPropertyName("value")]
        public double Value { get; set; } = 0.0d;
    }
}
