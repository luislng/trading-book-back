using TradingBook.Model.Currency;

namespace TradingBook.Model.Stock
{
    public record StockDto
    {
        public uint Id { get; set; }
        public StockReferenceDto StockReference { get; set; } = new StockReferenceDto();
        public decimal Price { get; set; } = 0.0M;
        public CurrencyDto Currency { get; set; } = new CurrencyDto();
        public DateTimeOffset BuyDate { get; set; }
        public decimal Amount { get; set; } = 0.0M;
        public decimal Fee { get; set; } = 0.0M;      
        public decimal Deposit { get; set; } = 0.0M;

        #region CURRENT_STATE

        public decimal CurrentPrice { get; set; } = 0.0M;
        public decimal PercentajeDiff { get; set; } = 0.0M;
        public string? RecomendedAction { get; set; }

        #endregion

        #region ALARMS_VALUES

        public decimal StopLoss { get; set; }
        public decimal SellLimit { get; set; }

        #endregion

        #region SELL

        public bool IsSelled { get; set; } = false;
        public DateTimeOffset? SellDate { get; set; }
        public decimal ReturnStockPrice { get; set; } = 0.0M;
        public decimal ReturnStockDiffPricePercentaje { get; set; } = 0.0M;
        public decimal ReturnFee { get; set; } = 0.0M;
        public decimal ReturnAmount { get; set; } = 0.0M;      
        public decimal ReturnEarn { get; set; } = 0.0M;
        public decimal ReturnDiffAmount { get; set; } = 0.0M;
       
        #endregion
    }
}
