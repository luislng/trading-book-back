using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TradingBook.ExternalServices.StockProvider.Abstract;

namespace TradingBook.ExternalServices.StockProvider
{
    internal class StockServiceManagerProvider : IStockServiceManager
    {
        private const string CACHE_STOCK_KEY = "StockCacheValue";

        private readonly IServiceProvider _serviceProvider;        
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<StockServiceManagerProvider> _log;

        private static MemoryCacheEntryOptions CacheOptions
        {
            get
            {
                return new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                };
            }
        }

        public StockServiceManagerProvider(IServiceProvider serviceProvider, IMemoryCache memoryCache,ILogger<StockServiceManagerProvider> log)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(IServiceProvider));            
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(IMemoryCache));
            _log = log ?? throw new ArgumentNullException(nameof(ILogger<StockServiceManagerProvider>));
        }

        public async Task<decimal> StockPrice(string stockCode)
        {
            string cacheKey = BuildCacheKey(stockCode);

            if(!_memoryCache.TryGetValue<decimal>(cacheKey, out decimal stockValue))
            {
                IEnumerable<IStockServiceProvider> stockServices = _serviceProvider.GetServices<IStockServiceProvider>();

                foreach (IStockServiceProvider stockService in stockServices)
                {
                    try
                    {
                        _log.LogInformation($"Calling to {stockService.GetType().Name} stock service");

                        decimal stockPrice = await stockService.RequestStockPrice(stockCode);

                        _memoryCache.Set<decimal>(cacheKey, stockPrice, CacheOptions);

                        return stockPrice;
                    }
                    catch (Exception e)
                    {
                        _log.LogError(e, "Error exchange rate service");
                    }
                }

                throw new Exception("Exchange rate services not availables");
            }

            return stockValue;
        }

        private static string BuildCacheKey(string stockCode) => $"{CACHE_STOCK_KEY}_{stockCode}";
    }
}
