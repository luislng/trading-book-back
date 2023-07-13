using AutoMapper;
using TradingBook.Model.Currency;
using TradingBook.Model.Entity;

namespace TradingBook.Application.Mapper
{
    internal class CurrencyMapper:MapperBaseProfile<CurrencyEntity, CurrencyDto>
    {
        protected override void CustomizeMap(IMappingExpression<CurrencyEntity, CurrencyDto> mappingExpression)
        {
            mappingExpression.ReverseMap();
        }
    }
}
