using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBook.Model.Asset;

namespace TradingBook.Application.Services.AssetService.Abstract
{
    public interface IAssetService
    {
        public AssetDto SaveAsset(AssetDto asset);
        public List<AssetDto> GetAssets();
        public void Delete(uint id);
        public AssetDto Find(uint id);
    }
}
