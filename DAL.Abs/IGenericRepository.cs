using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abs
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity GetByID(int? id);
        TEntity GetByID(int id);
        void Insert(TEntity entity);
        void Delete(int id);
        void Delete(int? id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);

    }
}
