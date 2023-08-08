using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBook.Model.CryptoCurrency
{
    public record CryptoCurrencyReferenceDto
    {
        public uint Id { get; private set; }    

        public string Name { get; set; }

        public string Code { get; set; }

        public void SetId(in uint id) => Id = id;
    }
}
