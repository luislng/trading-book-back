using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TradingBook.ExternalServices.ExchangeProvider.Abstract;

namespace TradingBook.ExternalServices.ExchangeProvider
{
    internal class ExchangeLoadBalancerProvider : ICurrencyExchangeServiceManager
    {
        private const string KEY_CACHE_EXCHANGE = "ExchangeValue";

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ExchangeLoadBalancerProvider> _log;   
        private readonly IMemoryCache _memoryCache;

        private MemoryCacheEntryOptions CacheOptions
        {
            get
            {
                return new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) 
                };     
            } 
        }

        public ExchangeLoadBalancerProvider(IServiceProvider serviceProvider, ILogger<ExchangeLoadBalancerProvider> log, IMemoryCache cache)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(IServiceProvider));
            _log = log ?? throw new ArgumentNullException(nameof(ILogger));
            _memoryCache = cache ?? throw new ArgumentNullException(nameof(IMemoryCache));
        }

        public async Task<decimal> ExchangeRate(string currencyCodeFrom, string currencyCodeTo)
        {
            if(!_memoryCache.TryGetValue<decimal>(BuildCacheKey(currencyCodeFrom, currencyCodeTo), out decimal exchangeCachedValue))
            {
                IEnumerable<ICurrencyExchangeService> exchangeServices = _serviceProvider.GetServices<ICurrencyExchangeService>();

                foreach (ICurrencyExchangeService currencyExchange in exchangeServices)
                {
                    try
                    {
                        decimal exchangeRate = await currencyExchange.ExchangeRate(currencyCodeFrom, currencyCodeTo);
                        
                        _memoryCache.Set<decimal>(BuildCacheKey(currencyCodeFrom, currencyCodeTo), exchangeRate, CacheOptions);

                        return exchangeRate;
                    }
                    catch (Exception e)
                    {
                        _log.LogError(e, "Error exchange rate service");
                    }
                }

                throw new Exception("Exchange rate services not availables");
            }

            return exchangeCachedValue;
        }

        private string BuildCacheKey(string currencyFrom, string currencyTo) => $"{KEY_CACHE_EXCHANGE}_{currencyFrom}_{currencyTo}";
    }
}
