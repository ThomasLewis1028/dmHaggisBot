using System.Linq;
using System.Threading.Tasks;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Database
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(string id);

        bool Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(string id);
    }
}