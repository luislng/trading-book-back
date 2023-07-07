using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradingBook.ExternalServices.ExchangeProvider.AlphaVantage.Model
{
    public class AlphaVantageRoot
    {
        [JsonExtensionData]
        public Dictionary<string,dynamic> Values { get; set; }
    }
}
