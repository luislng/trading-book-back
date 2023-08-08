namespace TradingBook.Model.Entity
{
    public class CryptoCurrencyReferenceEntity:EntityBase
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CryptoCurrencyEntity>? CryptoCurrenciesFrom { get; set; }

        public virtual ICollection<CryptoCurrencyEntity>? CryptoCurrenciesTo { get; set; }
    }
}
