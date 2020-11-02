using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDV<TEntity>
         where TEntity : BaseEntity
    {
        IList<TEntity> GetDVHEntities();

        TEntity GetDVHEntity(Guid entity);

        bool SetDVH(TEntity entity);
    }
}
