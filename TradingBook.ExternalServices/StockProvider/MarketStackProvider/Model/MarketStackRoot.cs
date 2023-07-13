using System.Text.Json.Serialization;

namespace TradingBook.ExternalServices.StockProvider.MarketStackProvider.Model
{
    public class MarketStackRoot
    {
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }

        [JsonPropertyName("data")]
        public List<Datum> Data { get; set; }
    }
}
