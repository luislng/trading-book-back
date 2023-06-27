using TradingBook.Model.Asset;
using TradingBook.Model.Currency;

namespace TradingBook.Model.Invest
{
    public record AddInvestDto
    {        
        public AssetDto Asset { get; set; } = new AssetDto();
        public double Price { get; set; } = 0.0d;
        public CurrencyDto CurrencyAsset { get; set; } = new CurrencyDto();    
        public double StopLoss { get; set; }
        public double SellLimit { get; set; }
        public double Amount { get; set; }
        public CurrencyDto CurrencyInvest { get; set; } = new CurrencyDto();
        public double Fee { get; set; }
    }
}
