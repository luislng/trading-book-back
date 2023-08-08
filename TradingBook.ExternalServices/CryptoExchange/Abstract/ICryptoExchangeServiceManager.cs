﻿using TradingBook.Model.CryptoCurrency;

namespace TradingBook.ExternalServices.CryptoExchange.Abstract
{
    public interface ICryptoExchangeServiceManager
    {
        public Task<CryptoExchangeSpotPrice> SpotPrice(string cryptoCode);
    }
}
