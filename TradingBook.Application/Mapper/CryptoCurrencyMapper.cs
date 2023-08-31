using AutoMapper;
using TradingBook.Model.CryptoCurrency;
using TradingBook.Model.Entity;

namespace TradingBook.Application.Mapper
{
    internal class CryptoCurrencyMapper:MapperBaseProfile<CryptoCurrencyEntity, CryptoDto>
    {
        private const string DATE_FORMAT = "dd-MM-yyyy";

        protected override void CustomizeMap(IMappingExpression<CryptoCurrencyEntity, CryptoDto> mappingExpression)
        {
            mappingExpression.ForMember(x => x.CryptoCurrencyFrom,
                                    y => y.MapFrom(cryptoRef => cryptoRef.CurrencyFrom == null ? String.Empty
                                                                                                              : cryptoRef.CurrencyFrom.Name));

            mappingExpression.ForMember(x => x.CryptoCurrencyTo,
                                   y => y.MapFrom(cryptoRef => cryptoRef.CurrencyTo == null ? String.Empty
                                                                                                           : cryptoRef.CurrencyTo.Name));
            mappingExpression.ForMember(x => x.CryptoReference,
                                   y => y.MapFrom(cryptoRef => cryptoRef.CryptoReference == null ? String.Empty
                                                                                                           : cryptoRef.CryptoReference.Name));

            mappingExpression.ForMember(x => x.BuyDate,
                                       y => y.MapFrom(cryptoRef => cryptoRef.BuyDate.ToString(DATE_FORMAT)));

            mappingExpression.ForMember(x => x.SellDate,
                                        y => y.MapFrom(cryptoRef => cryptoRef.SellDate.HasValue ? cryptoRef.SellDate.Value.ToString(DATE_FORMAT) : ""));

            mappingExpression.ReverseMap();
        }
    }
}
