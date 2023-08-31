using AutoMapper;
using TradingBook.Application.Services.StockReference.Abstract;
using TradingBook.ExternalServices.StockProvider.Abstract;
using TradingBook.Infraestructure.Repository.StockReferenceRepository;
using TradingBook.Infraestructure.UnitOfWork;
using TradingBook.Model.Stock;

namespace TradingBook.Application.Services.StockReference.Implementation
{
    internal class StockReferenceService : IStockReferenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStockServiceManager _stockProviderService;

        public StockReferenceService(IUnitOfWork unitOfWork, IMapper mapper, IStockServiceManager stockProviderService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            _stockProviderService = stockProviderService ?? throw new ArgumentNullException(nameof(IStockServiceManager));
        }

        public async Task<bool> CheckIfStockExists(string code)
        {
            return await _stockProviderService.CheckIfCodeExists(code);
        }

        public async Task DeleteAsync(uint id)
        {
            IStockReferenceRepository repository = _unitOfWork.GetRepository<IStockReferenceRepository>();
            await repository.RemoveAsync(id);

            _unitOfWork.SaveChanges();
        }

        public async Task<List<StockReferenceDto>> GetAllAssetsAsync()
        {
            IStockReferenceRepository repository = _unitOfWork.GetRepository<IStockReferenceRepository>();

            List<Model.Entity.StockReferenceEntity> assets = await repository.GetAllAsync();

            List<StockReferenceDto> assetDto = assets.Select(x => _mapper.Map<Model.Entity.StockReferenceEntity, StockReferenceDto>(x))
                                            .ToList();

            return assetDto;
        }

        public async Task<StockReferenceDto> SaveAssetAsync(StockReferenceDto asset)
        {
            IStockReferenceRepository repository = _unitOfWork.GetRepository<IStockReferenceRepository>();

            StockReferenceDto assetDto = asset;
            Model.Entity.StockReferenceEntity assetEntity = _mapper.Map<Model.Entity.StockReferenceEntity>(assetDto);

            await repository.AddAsync(assetEntity);

            _unitOfWork.SaveChanges();

            assetDto.SetId(assetEntity.Id);

            return assetDto;
        }
    }
}
