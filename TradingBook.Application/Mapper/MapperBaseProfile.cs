using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBook.Application.Mapper
{
    public abstract class MapperBaseProfile<Orig, Dest> : Profile where Orig : new() where Dest : new()
    {
        public MapperBaseProfile() { BuildBaseMap(); }

        private void BuildBaseMap()
        {
            IMappingExpression<Orig, Dest> mappingExpression = CreateMap<Orig, Dest>();
            CustomizeMap(mappingExpression);
        }

        protected virtual void CustomizeMap(IMappingExpression<Orig, Dest> mappingExpression) { }
    }
}
