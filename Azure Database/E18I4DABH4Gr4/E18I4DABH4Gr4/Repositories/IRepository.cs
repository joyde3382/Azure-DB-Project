using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E18I4DABH4Gr4.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(string id);
        IEnumerable<TEntity> GetAll();

        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);

        Task Set(TEntity entity);

        Task Remove(TEntity entity);
        Task RemoveRange(IEnumerable<TEntity> entities);
    }
}
