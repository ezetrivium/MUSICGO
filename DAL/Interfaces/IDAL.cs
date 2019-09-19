using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDAL<TEntity>
        where TEntity : BaseEntity
    {
        bool Add(TEntity entity);

        bool Delete(Guid id);

        IEnumerable<TEntity> Get();

        TEntity GetById(Guid id);

        bool Update(TEntity entity);
    }
}
