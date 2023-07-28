namespace TradingBook.ExternalServices.ExchangeProvider.Abstract
{
    public interface ICurrencyExchangeServiceManager
    {
        public Task<decimal> ExchangeRate(string currencyCodeFrom, string currencyCodeTo);
    }
}
