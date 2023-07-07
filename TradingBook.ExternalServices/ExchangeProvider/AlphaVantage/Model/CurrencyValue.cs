using System.Text.Json.Serialization;

namespace TradingBook.ExternalServices.ExchangeProvider.AlphaVantage.Model
{
    public class CurrencyValue
    {
        [JsonPropertyName("5. Exchange Rate")]
        public string ExchangeRate{ get; set; }
    }
}
