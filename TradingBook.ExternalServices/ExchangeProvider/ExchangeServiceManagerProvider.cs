using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TradingBook.ExternalServices.ExchangeProvider.Abstract;

namespace TradingBook.ExternalServices.ExchangeProvider
{
    internal class ExchangeServiceManagerProvider : ICurrencyExchangeServiceManager
    {
        private const string KEY_CACHE_EXCHANGE = "ExchangeValue";

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ExchangeServiceManagerProvider> _log;   
        private readonly IMemoryCache _memoryCache;

        private static MemoryCacheEntryOptions CacheOptions
        {
            get
            {
                return new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) 
                };     
            } 
        }

        public ExchangeServiceManagerProvider(IServiceProvider serviceProvider, ILogger<ExchangeServiceManagerProvider> log, IMemoryCache cache)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(IServiceProvider));
            _log = log ?? throw new ArgumentNullException(nameof(ILogger));
            _memoryCache = cache ?? throw new ArgumentNullException(nameof(IMemoryCache));
        }

        public async Task<decimal> ExchangeRate(string currencyCodeFrom, string currencyCodeTo)
        {
            if(!_memoryCache.TryGetValue<decimal>(BuildCacheKey(currencyCodeFrom, currencyCodeTo), out decimal exchangeCachedValue))
            {
                IEnumerable<ICurrencyExchangeServiceProvider> exchangeServices = _serviceProvider.GetServices<ICurrencyExchangeServiceProvider>();

                foreach (ICurrencyExchangeServiceProvider currencyExchange in exchangeServices)
                {
                    try
                    {
                        _log.LogInformation($"Calling to {currencyExchange.GetType().Name} currency service");

                        decimal exchangeRate = await currencyExchange.RequestExchangeRate(currencyCodeFrom, currencyCodeTo);
                        
                        _memoryCache.Set<decimal>(BuildCacheKey(currencyCodeFrom, currencyCodeTo), exchangeRate, CacheOptions);

                        return exchangeRate;
                    }
                    catch (Exception e)
                    {
                        _log.LogError(e, "Error exchange rate service");
                    }
                }

                throw new Exception("Exchange rate services not availables, see log for more info");
            }

            return exchangeCachedValue;
        }

        private static string BuildCacheKey(string currencyFrom, string currencyTo) => $"{KEY_CACHE_EXCHANGE}_{currencyFrom}_{currencyTo}";
    }
}
