using AutoMapper;
using TradingBook.Model.CryptoCurrency;
using TradingBook.Model.Entity;

namespace TradingBook.Application.Mapper
{
    internal class CryptoCurrencyReferenceMapper:MapperBaseProfile<CryptoCurrencyReferenceDto, CryptoCurrencyReferenceEntity>
    {
        protected override void CustomizeMap(IMappingExpression<CryptoCurrencyReferenceDto, CryptoCurrencyReferenceEntity> mappingExpression)
        {
            mappingExpression.ReverseMap();
        }
    }
}
