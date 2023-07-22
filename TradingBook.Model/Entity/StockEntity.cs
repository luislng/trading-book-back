namespace TradingBook.Model.Entity
{
    public class StockEntity: EntityBase
    {
        public uint StockReferenceId { get; set; }
        public StockReferenceEntity StockReference { get; set; } 
        public decimal Price { get; set; }
        public DateTimeOffset BuyDate { get; set; }
        public uint CurrencyId { get; set; }
        public CurrencyEntity Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        
        #region ALARMS_VALUES

        public decimal StopLoss { get; set; }
        public decimal SellLimit { get; set; }

        #endregion

        #region SELL_VALUES

        public bool IsSelled { get; set; } = false;
        public decimal ReturnAmount { get; set; } 
        public decimal ReturnFee { get; set; }
        public decimal ReturnStockPrice { get; set; }
        public DateTimeOffset? SellDate { get; set; } = null;

        #endregion
    }
}
