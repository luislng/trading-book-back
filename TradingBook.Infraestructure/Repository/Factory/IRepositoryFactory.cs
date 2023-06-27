using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBook.Infraestructure.Repository.Factory
{
    internal interface IRepositoryFactory
    {
        public TRepository GetInstance<TRepository>() where TRepository : class, IRepositoryBase;
    }
}
