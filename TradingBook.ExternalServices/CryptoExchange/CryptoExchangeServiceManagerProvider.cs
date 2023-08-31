using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TradingBook.ExternalServices.CryptoExchange.Abstract;
using TradingBook.Model.CryptoCurrency;

namespace TradingBook.ExternalServices.CryptoExchange
{
    internal class CryptoExchangeServiceManagerProvider : ICryptoExchangeServiceManager
    {
        private const string KEY_CACHE = "CryptoExchange";

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CryptoExchangeServiceManagerProvider> _log;
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

        public CryptoExchangeServiceManagerProvider(IServiceProvider serviceProvider, ILogger<CryptoExchangeServiceManagerProvider> log, IMemoryCache cache)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(IServiceProvider));
            _log = log ?? throw new ArgumentNullException(nameof(ILogger));
            _memoryCache = cache ?? throw new ArgumentNullException(nameof(IMemoryCache));
        }

        public async Task<CryptoExchangeSpotPrice> SpotPrice(string cryptoCode)
        {   
            if(!_memoryCache.TryGetValue(BuildCacheKey(cryptoCode), out CryptoExchangeSpotPrice spotPrice))
            {
                IEnumerable<ICryptoExchangeService> cryptoServices = _serviceProvider.GetServices<ICryptoExchangeService>();

                foreach (ICryptoExchangeService cryptoService in cryptoServices)
                {
                    try
                    {
                        CryptoExchangeSpotPrice price = await cryptoService.SpotPrice(cryptoCode);

                        _memoryCache.Set<CryptoExchangeSpotPrice>(BuildCacheKey(cryptoCode), price);

                        return price;                       
                    }
                    catch (Exception e)
                    {
                        _log.LogError(e.Message, e);
                    }
                }
            }

            return spotPrice;
        }

        public async Task<bool> CheckIfCodeExists(string cryptoCode)
        {
            try
            {
                CryptoExchangeSpotPrice price = await SpotPrice(cryptoCode);

                return price?.SpotPrice.HasValue ?? false;
            }
            catch (Exception) 
            {
                return false;
            }
        }

        private static string BuildCacheKey(string cryptoCode) => $"{KEY_CACHE}_{cryptoCode}";
    }  
}
