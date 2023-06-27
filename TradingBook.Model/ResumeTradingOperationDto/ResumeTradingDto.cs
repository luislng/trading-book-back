namespace TradingBook.Model.ResumeTradingOperationDto
{
    public record ResumeTradingDto
    {
        /// <summary>
        /// Gets or sets the deposit.
        /// </summary>
        /// <value>
        /// The deposit.
        /// </value>
        public double Deposit { get; set; } = 0.0d;

        /// <summary>
        /// Gets or sets the total earn.
        /// </summary>
        /// <value>
        /// The total earn.
        /// </value>
        public double TotalEarn { get; set; } = 0.0d;
    }
}
