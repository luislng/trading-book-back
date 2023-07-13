namespace TradingBook.Model.Stock
{
    public record SellStockDto
    {
        public uint StockId { get; set; }     
        public decimal Return { get; set; } = 0.0M;
        public decimal Fee { get; set; } = 0.0M;
        public decimal ReturnStockPrice { get; set; } = 0.0M;
    }
}
