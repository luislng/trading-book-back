using System.Text.Json.Serialization;

namespace TradingBook.ExternalServices.StockProvider.FinnHubProvider.Model
{
    public class FinHubRoot
    {
        [JsonPropertyName("c")]
        public double CurrenStockPrice { get; set; }
       
        [JsonPropertyName("d")]
        public double D { get; set; }
      
        [JsonPropertyName("dp")]
        public double Dp { get; set; }
       
        [JsonPropertyName("h")]
        public double H { get; set; }
      
        [JsonPropertyName("l")]
        public double L { get; set; }
       
        [JsonPropertyName("o")]
        public double O { get; set; }
      
        [JsonPropertyName("pc")]
        public double Pc { get; set; }
        
        [JsonPropertyName("t")]
        public int T { get; set; }
    }
}
