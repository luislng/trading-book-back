namespace TradingBook.Model.Entity
{
    public class CurrencyEntity : EntityBase
    {
        public string Name { get; set; } = String.Empty;

        public string Code { get; set; } = String.Empty;

        public virtual ICollection<StockEntity>? Stocks { get; set; }

        public virtual ICollection<CryptoCurrencyEntity>? CryptoCurrenciesFrom { get; set; }

        public virtual ICollection<CryptoCurrencyEntity>? CryptoCurrenciesTo { get; set; }
    }
}
