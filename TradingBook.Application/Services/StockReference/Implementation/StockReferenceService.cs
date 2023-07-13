﻿using AutoMapper;
using TradingBook.Application.Services.StockReference.Abstract;
using TradingBook.Infraestructure.Repository.StockReferenceRepository;
using TradingBook.Infraestructure.UnitOfWork;
using TradingBook.Model.Stock;

namespace TradingBook.Application.Services.StockReference.Implementation
{
    internal class StockReferenceService : IStockReferenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StockReferenceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        public void Delete(uint id)
        {
            IStockReferenceRepository repository = _unitOfWork.GetRepository<IStockReferenceRepository>();
            repository.Remove(id);

            _unitOfWork.SaveChanges();
        }

        public List<StockReferenceDto> GetAllAssets()
        {
            IStockReferenceRepository repository = _unitOfWork.GetRepository<IStockReferenceRepository>();

            List<Model.Entity.StockReferenceEntity> assets = repository.GetAll();

            List<StockReferenceDto> assetDto = assets.Select(x => _mapper.Map<Model.Entity.StockReferenceEntity, StockReferenceDto>(x))
                                            .ToList();

            return assetDto;
        }

        public StockReferenceDto SaveAsset(StockReferenceDto asset)
        {
            IStockReferenceRepository repository = _unitOfWork.GetRepository<IStockReferenceRepository>();

            StockReferenceDto assetDto = asset;
            Model.Entity.StockReferenceEntity assetEntity = _mapper.Map<Model.Entity.StockReferenceEntity>(assetDto);

            repository.Add(assetEntity);

            _unitOfWork.SaveChanges();

            assetDto.SetId(assetEntity.Id);

            return assetDto;
        }
    }
}