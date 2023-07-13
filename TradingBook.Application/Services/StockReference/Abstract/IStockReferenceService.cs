using TradingBook.Model.Stock;

namespace TradingBook.Application.Services.StockReference.Abstract
{
    public interface IStockReferenceService
    {
        public StockReferenceDto SaveAsset(StockReferenceDto asset);
        public List<StockReferenceDto> GetAllAssets();
        public void Delete(uint id);        
    }
}
