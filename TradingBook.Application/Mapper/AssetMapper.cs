using AutoMapper;
using TradingBook.Model.Asset;
using TradingBook.Model.Entity;

namespace TradingBook.Application.Mapper
{
    internal class AssetMapper:MapperBaseProfile<Asset, AssetDto>
    {
        protected override void CustomizeMap(IMappingExpression<Asset, AssetDto> mappingExpression)
        {
            mappingExpression.ReverseMap();
        }
    }
}
