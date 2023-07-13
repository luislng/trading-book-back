using TradingBook.Model.Currency;

namespace TradingBook.Application.Services.Currency.Abstract
{
    public interface ICurrencyService
    {
        public CurrencyDto SaveCurrency(CurrencyDto currency);

        public List<CurrencyDto> GetCurrencies();

        public void Delete(uint id);

        public Task<ExchangeDto> Exchange(decimal amount, string currencyCodeFrom, string currencyCodeTo);
    }
}
