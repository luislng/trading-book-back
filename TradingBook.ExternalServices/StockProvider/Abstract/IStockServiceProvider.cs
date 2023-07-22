namespace TradingBook.ExternalServices.StockProvider.Abstract
{
    public interface IStockServiceProvider
    {
        public Task<decimal> StockPrice(string stockCode);
    }
}
