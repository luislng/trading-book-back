using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly IStockServiceManager _stockProviderService;
        private readonly ILogger<StockService> _logger;

        public StockService(IUnitOfWork unitOfWork, IMapper mapper, IStockServiceManager stockProviderService,ILogger<StockService> log)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            _stockProviderService = stockProviderService ?? throw new ArgumentNullException(nameof(IStockServiceManager));
            _logger = log ?? throw new ArgumentNullException(nameof(ILogger));
        }

        public async Task<uint> SaveStockAsync(SaveStockDto invest)
        {
            IStockRepository repository =  _unitOfWork.GetRepository<IStockRepository>();

            StockEntity stockEntity =  _mapper.Map<StockEntity>(invest);

            stockEntity.BuyDate = DateTimeOffset.Now;

            await repository.AddAsync(stockEntity);

            _unitOfWork.SaveChanges();

            return stockEntity.Id;
        }

        public async Task<uint> UpdateMarketLimitAsync(MarketLimitsDto marketLimits)
        {
            IStockRepository repository = _unitOfWork.GetRepository<IStockRepository>();

            StockEntity stockEntity = await repository.FindAsync(marketLimits.StockId);

            stockEntity.StopLoss = marketLimits.StopLoss;
            stockEntity.SellLimit = marketLimits.SellLimit;

            _unitOfWork.SaveChanges();

            return stockEntity.Id;
        }

        public async Task<uint> SellAsync(SellStockDto sellInvest)
        {
            IStockRepository repository = _unitOfWork.GetRepository<IStockRepository>();

            StockEntity stockEntity = await repository.FindAsync(sellInvest.StockId);

            stockEntity.ReturnAmount = sellInvest.Return;
            stockEntity.ReturnFee = sellInvest.ReturnFee;
            stockEntity.ReturnStockPrice = sellInvest.ReturnStockPrice;
            stockEntity.IsSelled = true;
            stockEntity.SellDate = DateTimeOffset.Now;

            _unitOfWork.SaveChanges();

            return stockEntity.Id;
        }

        public async Task DeleteAsync(uint id)
        {
            IStockRepository repository = _unitOfWork.GetRepository<IStockRepository>();
            await repository.RemoveAsync(id);

            _unitOfWork.SaveChanges();
        }

        public async Task<List<StockDto>> GetAllAsync()
        {
            IStockRepository repository = _unitOfWork.GetRepository<IStockRepository>();

            IEnumerable<StockEntity> stockEntityCollection = await repository.GetAllAsync();

            var stockEntitiesTask = stockEntityCollection.Select(async x => await BuildStockAsync(x));

            List<StockDto> stockEntities = (await Task.WhenAll(stockEntitiesTask)).OrderByDescending(x=>x.BuyDate)
                                                                                  .ToList();

            return stockEntities;
        }

        public async Task<StockDto> GetByIdAsync(uint id)
        {
            IStockRepository repository = _unitOfWork.GetRepository<IStockRepository>();

            StockEntity stockEntity = await repository.FindAsync(id, trackEntity:false);

            StockDto stockDto = await BuildStockAsync(stockEntity);

            return stockDto;
        }

        private async Task<StockDto> BuildStockAsync(StockEntity stockEntity)
        {
            StockDto stockAux = _mapper.Map<StockDto>(stockEntity);

            try
            {
                stockAux.Deposit = stockAux.Amount - stockAux.Fee;

                ///TODO: Only for test purposes!
                //decimal stockPrice = await _stockProviderService.StockPrice(stockEntity.StockReference?.Code);
                decimal stockPrice = 100.256845M;
                stockAux.CurrentPrice = stockPrice;

                stockAux.PercentajeDiff = ((stockAux.CurrentPrice - stockAux.Price) / stockAux.Price) * 100M;

                if ((stockAux.CurrentPrice >= stockAux.SellLimit) || (stockAux.CurrentPrice <= stockAux.StopLoss))
                    stockAux.RecomendedAction = InvestActions.SELL;
                else
                    stockAux.RecomendedAction = InvestActions.HOLD;

                if (stockAux.IsSelled)
                {
                    stockAux.ReturnStockDiffPricePercentaje = ((stockAux.ReturnStockPrice - stockAux.Price) / stockAux.Price) * 100M;

                    stockAux.ReturnEarn = stockAux.ReturnAmount - stockAux.Amount;

                    stockAux.ReturnDiffAmount = ((stockAux.ReturnAmount - stockAux.Amount) / stockAux.Amount) * 100M;
                }

            }catch(Exception e)
            {
                _logger.LogError(e.Message);
            }

            return stockAux;
        }

        public async Task<decimal> TotalEarnedAsync()
        {
            IStockRepository stockRepository = _unitOfWork.GetRepository<IStockRepository>();
            decimal totalAmountEarned = (await stockRepository.GetAllAsync()).Sum(x => x.ReturnAmount);
            return totalAmountEarned;
        }
    }
}
