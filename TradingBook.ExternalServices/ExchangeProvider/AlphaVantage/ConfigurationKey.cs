namespace TradingBook.ExternalServices.ExchangeProvider.AlphaVantage
{
    internal static class ConfigurationKeyAlphaVantage
    {
        public const string URI = "Exchange:AlphaVantage:Uri";
        public const string API_KEY_HEADER = "Exchange:AlphaVantage:ApiKeyHeader";
        public const string API_KEY_VALUE = "Exchange:AlphaVantage:ApiKeyValue";        
        public const string PARAM_FROM_CURRENCY = "Exchange:AlphaVantage:ParamFromCurrency";
        public const string PARAM_TO_CURRENCY = "Exchange:AlphaVantage:ParamToCurrency";
        public const string FUNCTION_KEY = "function";
        public const string FUNCTION_VALUE = "CURRENCY_EXCHANGE_RATE";
    }
}
