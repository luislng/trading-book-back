using AutoMapper;
using TradingBook.Model.Currency;
using TradingBook.Model.Entity;
using TradingBook.Model.Stock;

namespace TradingBook.Application.Mapper
{
    internal class StockMapper : MapperBaseProfile<StockEntity, StockDto>
    {
        private const string DATE_FORMAT = "dd-MM-yyyy";

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

            mappingExpression.ForMember(x => x.BuyDate,
                                        y => y.MapFrom(stockRef => stockRef.BuyDate.ToString(DATE_FORMAT)));

            mappingExpression.ForMember(x => x.SellDate,
                                        y => y.MapFrom(stockRef => stockRef.SellDate.HasValue ? stockRef.SellDate.Value.ToString(DATE_FORMAT) : ""));

            mappingExpression.ReverseMap();
        }
    }
}
