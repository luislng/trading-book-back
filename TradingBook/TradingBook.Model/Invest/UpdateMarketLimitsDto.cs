using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBook.Model.Invest
{
    public record UpdateMarketLimitsDto
    {
        public double StopLoss { get; set; }    
        public double SellLimit { get; set; }   
    }
}
