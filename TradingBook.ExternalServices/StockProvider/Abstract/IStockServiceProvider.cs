namespace TradingBook.ExternalServices.StockProvider.Abstract
{
    internal interface IStockServiceProvider
    {
        public Task<decimal> RequestStockPrice(string stockCode);
    }
}
