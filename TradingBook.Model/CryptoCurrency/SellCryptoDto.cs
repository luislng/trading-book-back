namespace TradingBook.Model.CryptoCurrency
{
    public class SellCryptoDto
    {
        public uint CryptoCurrencyId { get; set; }

        public decimal ReturnPrice { get; set; }

        public decimal ReturnAmount { get; set; }

        public decimal ReturnFee { get; set; }
    }
}
