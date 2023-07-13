namespace TradingBook.ExternalServices.StockProvider.Abstract
{
    public interface IStockProviderService
    {
        public Task<decimal> StockPrice(string stockCode);
    }
}
