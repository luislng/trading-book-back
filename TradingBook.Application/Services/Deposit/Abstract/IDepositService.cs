namespace TradingBook.Application.Services.Deposit.Abstract
{
    public interface IDepositService
    {
        public Task AddDepositAsync (decimal amount);
        
        public Task<decimal> TotalDepositAmountAsync();
    }
}
