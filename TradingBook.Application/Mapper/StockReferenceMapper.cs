using AutoMapper;
using TradingBook.Model.Entity;
using TradingBook.Model.Stock;

namespace TradingBook.Application.Mapper
{
    internal class StockReferenceMapper:MapperBaseProfile<StockReferenceEntity, StockReferenceDto>
    {
        protected override void CustomizeMap(IMappingExpression<StockReferenceEntity, StockReferenceDto> mappingExpression)
        {
            mappingExpression.ReverseMap();
        }
    }
}
