namespace TradingBook.ExternalServices.ExchangeProvider.Abstract
{
    internal interface ICurrencyExchangeServiceProvider
    {
        public Task<decimal> RequestExchangeRate(string currencyCodeFrom, string currencyCodeTo);
    }
}
