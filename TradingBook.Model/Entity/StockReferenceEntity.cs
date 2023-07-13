namespace TradingBook.Model.Entity
{
    public class StockReferenceEntity : EntityBase
    {    
        public string Name { get; set; } = String.Empty;

        public string Code { get; set; } = String.Empty;

        public virtual ICollection<StockEntity> Stocks { get; set; }
    }
}
