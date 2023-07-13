using AutoMapper;
using TradingBook.Application.Services.Currency.Abstract;
using TradingBook.ExternalServices.ExchangeProvider.Abstract;
using TradingBook.Infraestructure.Repository.CurrencyRepository;
using TradingBook.Infraestructure.UnitOfWork;
using TradingBook.Model.Currency;
using TradingBook.Model.Entity;

namespace TradingBook.Application.Services.Currency.Implementation
{
    internal class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrencyExchangeServiceManager _exchangeService;

        public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper, ICurrencyExchangeServiceManager exchangeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            _exchangeService = exchangeService ?? throw new ArgumentNullException(nameof(ICurrencyExchangeService));
        }

        public void Delete(uint id)
        {
            ICurrencyRepository repository = _unitOfWork.GetRepository<ICurrencyRepository>();
            repository.Remove(id);

            _unitOfWork.SaveChanges();
        }
             
        public List<CurrencyDto> GetCurrencies()
        {
            ICurrencyRepository repository = _unitOfWork.GetRepository<ICurrencyRepository>();

            List<Model.Entity.CurrencyEntity> currencies = repository.GetAll();

            List<CurrencyDto> currencyDto = currencies.Select(x => _mapper.Map<CurrencyEntity, CurrencyDto>(x))
                                                      .ToList();
            return currencyDto;
        }

        public CurrencyDto SaveCurrency(CurrencyDto currency)
        {
            ICurrencyRepository repository = _unitOfWork.GetRepository<ICurrencyRepository>();

            CurrencyDto currencyDto = currency;
            CurrencyEntity currencyEntity = _mapper.Map<CurrencyEntity>(currencyDto);

            repository.Add(currencyEntity);

            _unitOfWork.SaveChanges();

            currencyDto.SetId(currencyEntity.Id);

            return currencyDto;
        }

        public async Task<ExchangeDto> Exchange(decimal amount, string currencyCodeFrom, string currencyCodeTo)
        {
            decimal exchangeRate = await _exchangeService.ExchangeRate(currencyCodeFrom, currencyCodeTo);

            decimal exchangeValue = amount * exchangeRate;  

            return new ExchangeDto() { ExchangeRate = exchangeValue };
        }
    }
}
