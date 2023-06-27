﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBook.Model.Currency
{
    public record CurrencyDto
    {
        public string Name { get; set; } = String.Empty;    
        public string Code { get; set; } = String.Empty;    
    }
}