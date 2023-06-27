using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBook.Model.Entity;

namespace TradingBook.Infraestructure.Repository
{
    public interface IEntityRepository<TEntity>: IRepositoryBase where TEntity : EntityBase
    {
        public void Add(TEntity entity);
        public void Remove(uint id);
        public void Update(uint id, TEntity entity);
        public void Find(uint id);
        public List<TEntity> GetAll();
    }
}
