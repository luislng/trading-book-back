using AutoMapper;
using Microsoft.Extensions.Logging;
using TradingBook.Application.Services.CryptoCurrency.Abstract;
using TradingBook.ExternalServices.CryptoExchange.Abstract;
using TradingBook.Infraestructure.Repository.CryptoCurrencyRepository;
using TradingBook.Infraestructure.UnitOfWork;
using TradingBook.Model.CryptoCurrency;
using TradingBook.Model.Entity;
using TradingBook.Model.Invest;

namespace TradingBook.Application.Services.CryptoCurrency.Implementation
{
    internal class CryptoCurrencyService : ICryptoCurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;        
        private readonly ILogger<CryptoCurrencyService> _logger;
        private readonly ICryptoExchangeServiceManager _cryptoExchangeServiceManager;

        public CryptoCurrencyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CryptoCurrencyService> log, ICryptoExchangeServiceManager cryptoExchangeServiceManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            _logger = log ?? throw new ArgumentNullException(nameof(IMapper));
            _cryptoExchangeServiceManager = cryptoExchangeServiceManager ?? throw new ArgumentNullException(nameof(ICryptoExchangeServiceManager));
        }

        public async Task DeleteAsync(uint id)
        {
            ICryptoCurrencyRepository repository = _unitOfWork.GetRepository<ICryptoCurrencyRepository>();
            await repository.RemoveAsync(id);

            _unitOfWork.SaveChanges();
        }

        public async Task<List<CryptoDto>> GetAllAsync()
        {
            ICryptoCurrencyRepository repository = _unitOfWork.GetRepository<ICryptoCurrencyRepository>();
            List<CryptoCurrencyEntity> cryptoCurrencyEntities = await repository.GetAllAsync();

            List<CryptoDto> cryptoCurrenciesCalculatedValues = await BuildCryptoWithCalculatedFields(cryptoCurrencyEntities);

            return cryptoCurrenciesCalculatedValues;
        }

        public async Task<CryptoDto> GetByIdAsync(uint id)
        {
            ICryptoCurrencyRepository repository = _unitOfWork.GetRepository<ICryptoCurrencyRepository>();
            CryptoCurrencyEntity cryptoCurrencyEntity = await repository.GetById(id);

            CryptoDto cryptoCurrenciesCalculatedValues = (await BuildCryptoWithCalculatedFields(new CryptoCurrencyEntity[] { cryptoCurrencyEntity })).Single();

            return cryptoCurrenciesCalculatedValues;
        }

        public async Task<uint> SaveCryptoCurrencyAsync(SaveCryptoDto invest)
        {
            ICryptoCurrencyRepository repository = _unitOfWork.GetRepository<ICryptoCurrencyRepository>();

            CryptoCurrencyEntity entitySaved = _mapper.Map<CryptoCurrencyEntity>(invest);
            entitySaved.BuyDate = DateTimeOffset.Now;
            
            await repository.AddAsync(entitySaved);

            _unitOfWork.SaveChanges();

            return entitySaved.Id;
        }

        public async Task<uint> SellAsync(SellCryptoDto sellCrypto)
        {
            ICryptoCurrencyRepository repository = _unitOfWork.GetRepository<ICryptoCurrencyRepository>();
            CryptoCurrencyEntity cryptoCurrencyEntity = await repository.GetById(sellCrypto.CryptoCurrencyId);

            cryptoCurrencyEntity.IsSelled = true;
            cryptoCurrencyEntity.ReturnPrice = sellCrypto.ReturnPrice;
            cryptoCurrencyEntity.SellDate = DateTimeOffset.Now;
            cryptoCurrencyEntity.ReturnAmount = sellCrypto.ReturnAmount;
            cryptoCurrencyEntity.ReturnFee = sellCrypto.ReturnFee;

            _unitOfWork.SaveChanges();

            return cryptoCurrencyEntity.Id;
        }

        public async Task<decimal> TotalEurEarnedAsync()
        {
            return 0;
           // ICryptoCurrencyRepository repository = _unitOfWork.GetRepository<ICryptoCurrencyRepository>();
           // List<CryptoCurrencyEntity> cryptoSelled = await repository.GetCryptoSelled();

           //decimal cryptoEarnedEuro = cryptoSelled.Where(x => x.CryptoCurrencyReferenceFrom.Code == "EUR")
           //                                       .Select(x=> ((x.ReturnAmount - x.ReturnFee) - x.AmountInvest))
           //                                       .Sum();



           // return cryptoEarnedEuro;
        }

        public async Task<uint> UpdateMarketLimitAsync(MarketLimitsDto marketLimits)
        {
            ICryptoCurrencyRepository repository = _unitOfWork.GetRepository<ICryptoCurrencyRepository>();

            CryptoCurrencyEntity cryptoEntity = await repository.GetById(marketLimits.CryptoCurrencyId);

            cryptoEntity.StopLoss = marketLimits.StopLoss;
            cryptoEntity.SellLimit = marketLimits.SellLimit;

            _unitOfWork.SaveChanges();

            return cryptoEntity.Id;
        }

        private async Task<List<CryptoDto>> BuildCryptoWithCalculatedFields(IEnumerable<CryptoCurrencyEntity> cryptoCurrencies)
        {
            List<CryptoDto> cryptoCurrenciesDtos = new List<CryptoDto>();

            foreach (CryptoCurrencyEntity cryptoCurrency in cryptoCurrencies.OrderByDescending(x=>x.BuyDate))
            {
                CryptoDto cryptoDto = _mapper.Map<CryptoDto>(cryptoCurrency);

                cryptoDto.Deposit = cryptoDto.AmountInvest - cryptoDto.FeeInvest;

                if (cryptoDto.IsSelled)
                {
                    cryptoDto.ReturnDiffPricePercentage = ((cryptoDto.ReturnPrice - cryptoDto.CryptoPrice) / cryptoDto.ReturnPrice) * 100M;
                    cryptoDto.ReturnAmountWithFee = cryptoDto.ReturnAmount - cryptoDto.ReturnFee;
                    cryptoDto.ReturnEarn = cryptoDto.ReturnAmountWithFee - cryptoDto.AmountInvest;
                    cryptoDto.ReturnDiffAmountEarnedPercentage = ((cryptoDto.ReturnAmount - cryptoDto.AmountInvest) / cryptoDto.ReturnAmount) * 100M;
                }
                else
                {
                    CryptoExchangeSpotPrice cryptoSpotPrice = await _cryptoExchangeServiceManager.SpotPrice(cryptoCurrency.CryptoReference.Code);
                    cryptoDto.CurrentPrice = cryptoSpotPrice?.SpotPrice;

                    if (cryptoDto.CryptoPrice != 0.0M && cryptoDto.CurrentPrice.HasValue)
                        cryptoDto.CurrentDiffPercentage = (decimal)((cryptoDto.CurrentPrice - cryptoDto.CryptoPrice) / cryptoDto.CryptoPrice) * 100M;

                    if (cryptoDto.CurrentPrice.HasValue)
                    {
                        if ((cryptoDto.CurrentPrice >= cryptoDto.SellLimit) || (cryptoDto.CurrentPrice <= cryptoDto.StopLoss))
                            cryptoDto.RecomendedAction = InvestActions.SELL;
                        else
                            cryptoDto.RecomendedAction = InvestActions.HOLD;
                    }
                }

                cryptoCurrenciesDtos.Add(cryptoDto);
            }

            return cryptoCurrenciesDtos;
        }
    }
}
