using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Database
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly UniverseContext _dbContext;

        public Repository(UniverseContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }
        public TEntity GetById(string id)
        {
            return _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
        }
        
        public bool Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            int result = _dbContext.SaveChanges();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return result == 1;
        }

        public bool Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            int result = _dbContext.SaveChanges();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return result == 1;
        }

        public bool Delete(string id)
        {
            var entity = GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            int result = _dbContext.SaveChanges();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return result == 1;
        }

        public void Dispose()
        {
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}