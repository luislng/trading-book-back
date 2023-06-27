using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBook.Model.Invest
{
    public record SellInvestDto
    {
        public double Return { get; set; } = 0.0d;
        public double Fee { get; set; } = 0.0d;
        public double AssetPrice { get; set; } = 0.0d;
    }
}
