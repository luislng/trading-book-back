using TradingBook.Model.Invest;
using TradingBook.Model.Stock;

namespace TradingBook.Application.Services.Stock.Abstract
{
    public interface IStockService
    {
        public Task<List<StockDto>> GetAllAsync();

        public Task<StockDto> GetByIdAsync(uint id);

        public Task<uint> SaveStockAsync(SaveStockDto invest);

        public Task<uint> UpdateMarketLimitAsync(MarketLimitsDto marketLimits);

        public Task<uint> SellAsync(SellStockDto sellInvest);

        public Task DeleteAsync(uint id);

        public Task<decimal> TotalEurEarnedAsync();
    }
}
