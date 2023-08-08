using AutoMapper;
using Microsoft.Extensions.Logging;
using TradingBook.Application.Services.Stock.Abstract;
using TradingBook.ExternalServices.ExchangeProvider.Abstract;
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
        private readonly ICurrencyExchangeServiceManager _exchangeService;  

        public StockService(IUnitOfWork unitOfWork, IMapper mapper, IStockServiceManager stockProviderService,ILogger<StockService> log,
                            ICurrencyExchangeServiceManager exchangeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            _stockProviderService = stockProviderService ?? throw new ArgumentNullException(nameof(IStockServiceManager));
            _logger = log ?? throw new ArgumentNullException(nameof(ILogger));
            _exchangeService = exchangeService ?? throw new ArgumentNullException(nameof(ICurrencyExchangeServiceManager));
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

            stockEntity.IsSelled = true;
            stockEntity.ReturnAmount = sellInvest.Return;
            stockEntity.ReturnFee = sellInvest.ReturnFee;
            stockEntity.ReturnStockPrice = sellInvest.ReturnStockPrice;            
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

                if (!stockAux.IsSelled)
                {
                    decimal stockPrice = await _stockProviderService.StockPrice(stockEntity.StockReference?.Code);

                    stockAux.CurrentPrice = stockPrice;

                    if(stockAux.Price != 0.0M)
                        stockAux.PercentajeDiff = ((stockAux.CurrentPrice - stockAux.Price) / stockAux.Price) * 100M;

                    if ((stockAux.CurrentPrice >= stockAux.SellLimit) || (stockAux.CurrentPrice <= stockAux.StopLoss))
                        stockAux.RecomendedAction = InvestActions.SELL;
                    else
                        stockAux.RecomendedAction = InvestActions.HOLD;
                }                

                if (stockAux.IsSelled)
                {
                    stockAux.ReturnStockDiffPricePercentaje = ((stockAux.ReturnStockPrice - stockAux.Price) / stockAux.Price) * 100M;

                    stockAux.ReturnAmountWithFee = stockAux.ReturnAmount - stockAux.ReturnFee;

                    stockAux.ReturnEarn = stockAux.ReturnAmountWithFee - stockAux.Amount;

                    stockAux.ReturnDiffAmount = ((stockAux.ReturnAmountWithFee - stockAux.Amount) / stockAux.Amount) * 100M;
                }

            }catch(Exception e)
            {
                _logger.LogError(e.Message);
            }

            return stockAux;
        }

        public async Task<decimal> TotalEurEarnedAsync()
        {
            const string EUR_CURRENCY_CODE = "EUR";

            decimal totalAmountEarned = 0.0M;

            IStockRepository stockRepository = _unitOfWork.GetRepository<IStockRepository>();            

            List<StockEntity> stockSells = (await stockRepository.GetAllAsync()).Where(x => x.IsSelled)
                                                                                .ToList();

            foreach (var stockAux in stockSells)
            {
                decimal exchangeRate = 1.0M;

                if(stockAux.Currency.Code != EUR_CURRENCY_CODE)
                     exchangeRate = await _exchangeService.ExchangeRate(stockAux.Currency.Code, EUR_CURRENCY_CODE);

                totalAmountEarned += (stockAux.ReturnAmount - stockAux.ReturnFee - stockAux.Amount) * exchangeRate;
            }                                                               

            return totalAmountEarned;
        }
    }
}
