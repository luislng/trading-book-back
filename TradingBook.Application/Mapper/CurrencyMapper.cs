using AutoMapper;
using TradingBook.Model.Currency;
using TradingBook.Model.Entity;

namespace TradingBook.Application.Mapper
{
    internal class CurrencyMapper:MapperBaseProfile<Currency, CurrencyDto>
    {
        protected override void CustomizeMap(IMappingExpression<Currency, CurrencyDto> mappingExpression)
        {
            mappingExpression.ReverseMap();
        }
    }
}
