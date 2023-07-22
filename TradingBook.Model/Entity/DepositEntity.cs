namespace TradingBook.Model.Entity
{
    public class DepositEntity:EntityBase
    {
        public decimal Deposit { get; set; }

        public DateTimeOffset DepositDate { get; set; } 
    }
}
