﻿namespace TradingBook.Model.Currency
{
    public record CurrencyDto
    {
        public CurrencyDto() { }

        public CurrencyDto(uint id)
        {
           this.Id = id;   
        }

        public uint Id { get; internal set; }        

        public string Name { get; set; } = String.Empty;    

        public string Code { get; set; } = String.Empty;

        public void SetId(in uint Id) => this.Id = Id;
    }
}
