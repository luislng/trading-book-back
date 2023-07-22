using AutoMapper;
using TradingBook.Application.Services.Deposit.Abstract;
using TradingBook.Infraestructure.Repository.DepositRepository;
using TradingBook.Infraestructure.Repository.StockRepository;
using TradingBook.Infraestructure.UnitOfWork;
using TradingBook.Model.Entity;

namespace TradingBook.Application.Services.Deposit.Implementation
{
    internal class DepositService : IDepositService
    {
        private readonly IUnitOfWork _unitOfWork;              

        public DepositService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork));            
        }

        public async Task AddDepositAsync(decimal amount)
        {
            IDepositRepository repository = _unitOfWork.GetRepository<IDepositRepository>();

            DepositEntity depositEntity = new DepositEntity()
            {
                Deposit = amount,
                DepositDate = DateTimeOffset.Now
            };

            await repository.AddAsync(depositEntity);

            _unitOfWork.SaveChanges();
        }

        public async Task<decimal> TotalDepositAmountAsync()
        {
            IDepositRepository repository = _unitOfWork.GetRepository<IDepositRepository>();

            List<DepositEntity> depositEntities = await repository.GetAllAsync();

            decimal totalAmountDeposites = depositEntities.Sum(x => x.Deposit);

            return totalAmountDeposites;
        }

       
    }
}
