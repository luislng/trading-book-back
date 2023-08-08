using AutoMapper;
using Microsoft.Extensions.Logging;
using TradingBook.Application.Services.CryptoCurrencyReference.Abstract;
using TradingBook.Infraestructure.Repository.CryptoCurrencyReferenceRepository;
using TradingBook.Infraestructure.UnitOfWork;
using TradingBook.Model.CryptoCurrency;
using TradingBook.Model.Entity;

namespace TradingBook.Application.Services.CryptoCurrencyReference.Implementation
{
    internal class CryptoCurrencyReferenceService : ICryptoCurrencyReferenceService
    {
        private readonly ILogger<CryptoCurrencyReferenceService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CryptoCurrencyReferenceService(ILogger<CryptoCurrencyReferenceService> logger,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        public async Task DeleteAsync(uint id)
        {
            ICryptoCurrencyReferenceRepository repository = _unitOfWork.GetRepository<ICryptoCurrencyReferenceRepository>();

            await repository.RemoveAsync(id);

            _unitOfWork.SaveChanges();
        }

        public async Task<List<CryptoCurrencyReferenceDto>> GetAllAsync()
        {
            ICryptoCurrencyReferenceRepository repository = _unitOfWork.GetRepository<ICryptoCurrencyReferenceRepository>();

            List<CryptoCurrencyReferenceEntity> cryptoCurrencyReferenceEntities = await repository.GetAllAsync();

            List<CryptoCurrencyReferenceDto> cryptoCurrencyReferenceDtos = cryptoCurrencyReferenceEntities.Select(x => _mapper.Map<CryptoCurrencyReferenceDto>(x))
                                                                                                          .ToList();

            return cryptoCurrencyReferenceDtos; 
        }

        public async Task<CryptoCurrencyReferenceDto> SaveAsync(CryptoCurrencyReferenceDto cryptoRefDto)
        {
            ICryptoCurrencyReferenceRepository repository = _unitOfWork.GetRepository<ICryptoCurrencyReferenceRepository>();

            CryptoCurrencyReferenceDto cryptoCurrencyReferenceDto = cryptoRefDto;
            CryptoCurrencyReferenceEntity currencyReferenceEntity = _mapper.Map<CryptoCurrencyReferenceEntity>(cryptoCurrencyReferenceDto);

            await repository.AddAsync(currencyReferenceEntity);

            _unitOfWork.SaveChanges();

            cryptoCurrencyReferenceDto.SetId(currencyReferenceEntity.Id);

            return cryptoCurrencyReferenceDto;
        }
    }
}
