namespace TradingBook.Model.Stock
{
    public record SaveStockDto
    {
        public uint Id { get; private set; }
        public void SetId(in uint Id) => this.Id = Id;
        public uint StockReferenceId { get; set; }
        public decimal Price { get; set; }
        public uint CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal StopLoss { get; set; }
        public decimal SellLimit { get; set; }       
    }
}
