using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TradingBook.ExternalServices.ExchangeProvider.ApiExchange.Model
{
    internal sealed class CurrencyApiExchangeDto
    {
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; } = new Meta();

        [JsonPropertyName("data")]
        public dynamic Data { get; set; }
    }
}
