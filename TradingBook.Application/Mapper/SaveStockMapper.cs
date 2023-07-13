using AutoMapper;
using TradingBook.Model.Entity;
using TradingBook.Model.Stock;

namespace TradingBook.Application.Mapper
{
    internal class SaveStockMapper : MapperBaseProfile<StockEntity, SaveStockDto>
    {
        protected override void CustomizeMap(IMappingExpression<StockEntity, SaveStockDto> mappingExpression)
        {
            mappingExpression.ReverseMap();
        }
    }
}
