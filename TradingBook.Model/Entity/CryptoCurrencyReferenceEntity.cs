namespace TradingBook.Model.Entity
{
    public class CryptoCurrencyReferenceEntity:EntityBase
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CryptoCurrencyEntity>? CryptoCurrencyReferences { get; set; }
    }
}
