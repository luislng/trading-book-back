namespace TradingBook.ExternalServices.StockProvider.Abstract
{
    public interface IStockServiceManager
    {
        public Task<decimal> StockPrice(string stockCode);

        public Task<bool> CheckIfCodeExists(string stockCode);
    }
}
