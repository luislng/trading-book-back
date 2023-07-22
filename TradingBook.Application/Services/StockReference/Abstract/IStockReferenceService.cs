using TradingBook.Model.Stock;

namespace TradingBook.Application.Services.StockReference.Abstract
{
    public interface IStockReferenceService
    {
        public Task<StockReferenceDto> SaveAssetAsync(StockReferenceDto asset);
        public Task<List<StockReferenceDto>> GetAllAssetsAsync();
        public Task DeleteAsync(uint id);        
    }
}
