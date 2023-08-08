using AutoMapper;
using TradingBook.Model.CryptoCurrency;
using TradingBook.Model.Entity;

namespace TradingBook.Application.Mapper
{
    internal class SaveCryptoMapper : MapperBaseProfile<SaveCryptoDto, CryptoCurrencyEntity>
    {
        protected override void CustomizeMap(IMappingExpression<SaveCryptoDto, CryptoCurrencyEntity> mappingExpression)
        {
            mappingExpression.ReverseMap();
        }
    }
}
