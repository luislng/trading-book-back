using TradingBook.Model.Currency;

namespace TradingBook.Application.Services.Currency.Abstract
{
    public interface ICurrencyService
    {
        public Task<CurrencyDto> SaveCurrencyAsync(CurrencyDto currency);

        public Task<List<CurrencyDto>> GetCurrenciesAsync();

        public Task DeleteAsync(uint id);

        public Task<ExchangeDto> ExchangeAsync(decimal amount, string currencyCodeFrom, string currencyCodeTo);
    }
}
