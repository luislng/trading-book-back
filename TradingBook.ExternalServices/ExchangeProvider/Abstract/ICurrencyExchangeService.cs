namespace TradingBook.ExternalServices.ExchangeProvider.Abstract
{
    public interface ICurrencyExchangeService
    {
        public Task<decimal> ExchangeRate(string currencyCodeFrom, string currencyCodeTo);
    }
}
