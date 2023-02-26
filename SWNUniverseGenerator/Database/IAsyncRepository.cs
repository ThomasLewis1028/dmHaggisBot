using System.Linq;
using System.Threading.Tasks;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Database
{
    public interface IAsyncRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(string id);

        Task Add(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(string id);
    }
}