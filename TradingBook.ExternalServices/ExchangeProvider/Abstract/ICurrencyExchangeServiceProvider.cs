namespace TradingBook.ExternalServices.ExchangeProvider.Abstract
{
    public interface ICurrencyExchangeServiceProvider
    {
        public Task<decimal> ExchangeRate(string currencyCodeFrom, string currencyCodeTo);
    }
}
