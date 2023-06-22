using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBook.Model.Currency;

namespace TradingBook.Model.Invest
{
    public record InvestDto
    {
        public uint Id { get; set; }
        public string AssetName { get; set; } = String.Empty;
        public double Price { get; set; } = 0.0d;
        public CurrencyDto AssetCurrency { get; set; }  = new CurrencyDto();
        public DateTimeOffset AssetBuy { get; set; }
        public double InvestAmount { get; set; } = 0.0d;
        public CurrencyDto InvestCurrency { get; set; } = new CurrencyDto();
        public double InvestFee { get; set; } = 0.0d;
        public double InvestDeposit { get; set; } = 0.0d;
        public double AssetCurrentPrice { get; set; } = 0.0d;
        public double AssetPercentajeDiff { get; set; } = 0.0d;
        public double ReturnAssetPrice { get; set; } = 0.0d;
        public double ReturnAssetPricePercentajeDiff { get; set; } = 0.0d;
        public DateTimeOffset AssetReturn { get; set; }
        public double ReturnFee { get; set; } = 0.0d;
        public double ReturnTotal { get; set; } = 0.0d;
        public double ReturnEarn { get; set; } = 0.0d;
        public double ReturnDiffDepot { get; set; } = 0.0d;
        public double ReturnDiffAmount { get; set; } = 0.0d;
        public bool IsSelled { get; set; } = false;
        public string RecomendedAction { get; set; } = InvestActions.BUY;
    }
}
