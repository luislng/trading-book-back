using System.Text.Json.Serialization;

namespace TradingBook.ExternalServices.CryptoExchange.Binance.Model
{
    internal class BinanceRoot
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }
    }
}
