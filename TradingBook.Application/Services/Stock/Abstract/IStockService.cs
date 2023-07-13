using TradingBook.Model.Invest;
using TradingBook.Model.Stock;

namespace TradingBook.Application.Services.Stock.Abstract
{
    public interface IStockService
    {
        public Task<List<StockDto>> GetAll();

        public Task<StockDto> SaveStock(SaveStockDto invest);

        public Task<StockDto> SetMarketLimit(MarketLimitsDto marketLimits);

        public Task<StockDto> Sell(SellStockDto sellInvest);

        public void Delete(uint id);
    }
}
