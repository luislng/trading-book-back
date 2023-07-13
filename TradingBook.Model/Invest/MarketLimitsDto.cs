namespace TradingBook.Model.Invest
{
    public record MarketLimitsDto
    {
        public uint StockId { get; set; }
        public decimal StopLoss { get; set; }    
        public decimal SellLimit { get; set; }   
    }
}
