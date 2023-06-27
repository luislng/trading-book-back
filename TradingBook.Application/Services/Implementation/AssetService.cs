using AutoMapper;
using TradingBook.Application.Services.Abstract;
using TradingBook.Infraestructure.Repository.AssetRepository;
using TradingBook.Infraestructure.UnitOfWork;
using TradingBook.Model.Asset;
using TradingBook.Model.Entity;

namespace TradingBook.Application.Services.Implementation
{
    internal class AssetService : IAssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssetService(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        public void Delete(uint id)
        {
            IAssetRepository repository = _unitOfWork.GetRepository<IAssetRepository>();
            repository.Remove(id);

            _unitOfWork.SaveChanges();
        }

        public AssetDto Find(uint id)
        {
            throw new NotImplementedException();
        }

        public List<AssetDto> GetAssets()
        {
            IAssetRepository repository = _unitOfWork.GetRepository<IAssetRepository>();

            List<Asset> assets = repository.GetAll();

            List<AssetDto> assetDto = assets.Select(x => _mapper.Map<Asset, AssetDto>(x))
                                            .ToList();

            return assetDto;    
        }

        public AssetDto SaveAsset(AssetDto asset)
        {
            IAssetRepository repository = _unitOfWork.GetRepository<IAssetRepository>();

            AssetDto assetDto = asset;
            Asset assetEntity = _mapper.Map<Asset>(assetDto);

            repository.Add(assetEntity);

            _unitOfWork.SaveChanges();

            assetDto.Id = assetEntity.Id;

            return assetDto;
        }
    }
}
