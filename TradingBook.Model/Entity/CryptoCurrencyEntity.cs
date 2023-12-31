﻿namespace TradingBook.Model.Entity
{
    public class CryptoCurrencyEntity : EntityBase
    {
        public uint CurrencyFromId { get; set; }    

        public CurrencyEntity CurrencyFrom { get; set; }

        public uint CurrencyToId { get; set; }

        public CurrencyEntity CurrencyTo { get; set; }

        public uint CryptoReferenceId { get; set; }

        public CryptoCurrencyReferenceEntity CryptoReference { get; set; }

        public decimal AmountInvest { get; set; }

        public decimal FeeInvest { get; set; }

        public decimal CryptoPrice { get; set; }

        public decimal ExchangedAmount { get; set; }    

        public DateTimeOffset BuyDate { get; set; }        

        #region SELL
        
        public bool IsSelled { get; set; } = false;

        public decimal ReturnPrice { get; set; }    

        public DateTimeOffset? SellDate { get; set; }  

        public decimal ReturnAmount { get; set; }

        public decimal ReturnFee { get; set; }  

        #endregion

        #region LIMITS_REFERENCE

        public decimal StopLoss { get; set; }

        public decimal SellLimit { get; set; }
        
        #endregion
    }
}
