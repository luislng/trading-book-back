namespace TradingBook.Model.CryptoCurrency
{
    public class CryptoExchangeSpotPrice
    {
        public string CryptoCode { get; set; } = String.Empty;

        public decimal? SpotPrice { get; set; } = null;
    }
}
