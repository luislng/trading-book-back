namespace TradingBook.Model.CryptoCurrency
{
    public record SaveCryptoDto
    {
        public uint CryptoCurrencyReferenceFromId { get; set; }
        
        public uint CryptoCurrencyReferenceToId { get; set; }

        public decimal AmountInvest { get; set; }

        public decimal FeeInvest { get; set; }

        public decimal CryptoPrice { get; set; }

        public decimal ExchangedAmount { get; set; }

        public decimal StopLoss { get; set; }   

        public decimal SellLimit { get; set;}
    }
}
