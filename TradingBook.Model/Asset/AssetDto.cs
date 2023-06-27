using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradingBook.Model.Asset
{
    public record AssetDto
    {
        [JsonIgnore]
        public uint Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Code { get; set; } = String.Empty;        
    }
}
