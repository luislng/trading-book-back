using AutoMapper;
using TradingBook.Model.Currency;
using TradingBook.Model.Entity;
using TradingBook.Model.Stock;

namespace TradingBook.Application.Mapper
{
    internal class StockMapper : MapperBaseProfile<StockEntity, StockDto>
    {
        protected override void CustomizeMap(IMappingExpression<StockEntity, StockDto> mappingExpression)
        {
            mappingExpression.ForMember(x => x.StockReference, 
                                        y => y.MapFrom(stockRef => stockRef.StockReference == null ? new StockReferenceDto()
                                                                                                   : new StockReferenceDto(stockRef.StockReference.Id)
                                                                                                   {
                                                                                                       Code = stockRef.StockReference.Code,
                                                                                                       Name = stockRef.StockReference.Name
                                                                                                   }));

            mappingExpression.ForMember(x => x.Currency,
                                       y => y.MapFrom(stockRef => stockRef.Currency == null ? new CurrencyDto()
                                                                                                  : new CurrencyDto(stockRef.Currency.Id)
                                                                                                  {
                                                                                                      Code = stockRef.Currency.Code,
                                                                                                      Name = stockRef.Currency.Name
                                                                                                  }));

            mappingExpression.ReverseMap();
        }
    }
}
