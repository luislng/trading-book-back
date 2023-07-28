using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBook.ExternalServices.StockProvider.FinnHubProvider
{
    internal static class FinHubConfigurationKey
    {
        public const string URI = "Stock:FinHub:Uri";
        public const string KEY_PARAM = "Stock:FinHub:KeyParam";
        public const string KEY_VALUE = "Stock:FinHub:KeyValue";
        public const string SYMBOL_PARAM = "Stock:FinHub:SymbolParam";
    }
}
