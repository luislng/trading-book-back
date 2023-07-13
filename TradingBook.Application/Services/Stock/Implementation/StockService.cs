using AutoMapper;
using TradingBook.Application.Services.Stock.Abstract;
using TradingBook.ExternalServices.StockProvider.Abstract;
using TradingBook.Infraestructure.Repository.StockRepository;
using TradingBook.Infraestructure.UnitOfWork;
using TradingBook.Model.Entity;
using TradingBook.Model.Invest;
using TradingBook.Model.Stock;

namespace TradingBook.Application.Services.Stock.Implementation
{
    internal class StockService : IStockService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStockProviderService _stockProviderService;

        public StockService(IUnitOfWork unitOfWork, IMapper mapper, IStockProviderService stockProviderService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            _stockProviderService = stockProviderService ?? throw new ArgumentNullException(nameof(IStockProviderService));
        }

        public async Task<StockDto> SaveStock(SaveStockDto invest)
        {
            IStockRepository repository =  _unitOfWork.GetRepository<IStockRepository>();

            StockEntity stockEntity =  _mapper.Map<StockEntity>(invest);

            stockEntity.BuyDate = DateTimeOffset.Now;

            repository.Add(stockEntity);

            _unitOfWork.SaveChanges();

            StockDto stockDto = await GetBuildedStockById(stockEntity.Id);

            return stockDto;
        }

        public async Task<StockDto> SetMarketLimit(MarketLimitsDto marketLimits)
        {
            IStockRepository repository = _unitOfWork.GetRepository<IStockRepository>();

            StockEntity stockEntity = repository.Find(marketLimits.StockId);

            stockEntity.StopLoss = marketLimits.StopLoss;
            stockEntity.SellLimit = marketLimits.SellLimit;

            _unitOfWork.SaveChanges();

            StockDto stockDto = await GetBuildedStockById(stockEntity.Id);

            return stockDto;
        }

        public async Task<StockDto> Sell(SellStockDto sellInvest)
        {
            IStockRepository repository = _unitOfWork.GetRepository<IStockRepository>();

            StockEntity stockEntity = repository.Find(sellInvest.StockId);

            stockEntity.ReturnAmount = sellInvest.Return;
            stockEntity.Fee = sellInvest.Fee;
            stockEntity.ReturnStockPrice = sellInvest.ReturnStockPrice;
            stockEntity.IsSelled = true;
            stockEntity.SellDate = DateTimeOffset.Now;

            _unitOfWork.SaveChanges();

            StockDto stockDto = await GetBuildedStockById(stockEntity.Id);

            return stockDto;
        }

        public void Delete(uint id)
        {
            IStockRepository repository = _unitOfWork.GetRepository<IStockRepository>();
            repository.Remove(id);

            _unitOfWork.SaveChanges();
        }

        public async Task<List<StockDto>> GetAll()
        {
            IStockRepository repository = _unitOfWork.GetRepository<IStockRepository>();

            IEnumerable<StockEntity> stockEntityCollection = repository.GetAll();

            var stockEntitiesTask = stockEntityCollection.Select(async x => await BuildStock(x));

            List<StockDto> stockEntities = (await Task.WhenAll(stockEntitiesTask)).ToList();

            return stockEntities;
        }

        private async Task<StockDto> GetBuildedStockById(uint id)
        {
            IStockRepository repository = _unitOfWork.GetRepository<IStockRepository>();

            StockEntity stockEntity = repository.Find(id, trackEntity:false);

            StockDto stockDto = await BuildStock(stockEntity);

            return stockDto;
        }

        private async Task<StockDto> BuildStock(StockEntity stockEntity)
        {
            StockDto stockAux = _mapper.Map<StockDto>(stockEntity);

            stockAux.Deposit = stockAux.Amount - stockAux.Fee;            

            decimal stockPrice = await _stockProviderService.StockPrice(stockEntity.StockReference?.Code);
            stockAux.CurrentPrice = stockPrice;
            stockAux.PercentajeDiff = ((stockAux.CurrentPrice - stockAux.Price) / stockAux.Price) * 100M;

            if (stockAux.CurrentPrice >= stockAux.SellLimit)
                stockAux.RecomendedAction = InvestActions.SELL;
            else
                if(stockAux.CurrentPrice <= stockAux.StopLoss)
                    stockAux.RecomendedAction = InvestActions.SELL;
            
            stockAux.RecomendedAction = InvestActions.HOLD;

            stockAux.ReturnStockDiffPricePercentaje = ((stockAux.ReturnStockPrice - stockAux.Price) / stockAux.Price) * 100M;

            stockAux.ReturnEarn = stockAux.ReturnAmount - stockAux.Amount;

            stockAux.ReturnDiffAmount = ((stockAux.ReturnAmount - stockAux.Amount) / stockAux.Amount) * 100M;

            return stockAux;
        }
    }
}
