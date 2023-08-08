namespace TradingBook.Model.CryptoCurrency
{
    public record CryptoDto
    {
        public uint Id { get; set; }

        public string CryptoCurrencyFrom { get; set; }

        public string CryptoCurrencyTo { get; set; }

        public decimal AmountInvest { get; set; }

        public decimal FeeInvest { get; set; }

        public decimal CryptoPrice { get; set; }

        public decimal Deposit { get ; set; }   

        public string BuyDate { get; set; }

        public decimal ExchangedAmount { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal CurrentDiffPercentage { get; set; }

        public string RecomendedAction { get; set; } = String.Empty;

        public bool IsSelled { get; set; } = false;

        public decimal ReturnPrice { get; set; }

        public decimal ReturnDiffPricePercentage { get; set; }

        public string? SellDate { get; set; }

        public decimal ReturnAmount { get; set; }

        public decimal ReturnFee { get; set; }

        public decimal ReturnAmountWithFee { get; set; }

        public decimal ReturnEarn { get; set; }

        public decimal ReturnDiffAmountEarnedPercentage { get; set; }

        public decimal StopLoss { get; set; }

        public decimal SellLimit { get; set; }
    }
}
