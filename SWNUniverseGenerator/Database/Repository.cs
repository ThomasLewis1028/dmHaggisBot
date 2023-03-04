using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IEnumerable<IEntity> Search(Expression<System.Func<TEntity, Boolean>> query)
        {
            return _dbContext.Set<TEntity>()
                .Where(query);
        }

        public IEntity Random()
        {
            var count = _dbContext.Set<TEntity>().Count();
            var rand = new Random().Next(0, count);
            return _dbContext.Set<TEntity>()
                .AsEnumerable()
                .ElementAt(rand);
        }

        public IEntity Random(Expression<System.Func<TEntity, Boolean>> query)
        {
            var count = _dbContext.Set<TEntity>()
                .Where(query)
                .Count();
            return _dbContext.Set<TEntity>()
                .Where(query)
                .AsEnumerable()
                .ElementAt(new Random().Next(0, count));
        }

        public Boolean Any(Expression<System.Func<TEntity, Boolean>> query)
        {
            return _dbContext.Set<TEntity>()
                .Any(query);
        }


        public Int32 Count(Expression<System.Func<TEntity, Boolean>> query)
        {
            return _dbContext.Set<TEntity>()
                .Count(query);
        }

        public TEntity GetById(string id)
        {
            return _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Add an entity to a list of entities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            int result = _dbContext.SaveChanges();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return result == 1;
        }

        public bool AddRange(List<TEntity> entity)
        {
            int count = entity.Count;
            _dbContext.Set<TEntity>().AddRange(entity.AsEnumerable());
            int result = _dbContext.SaveChanges();
            foreach (var e in entity)
                _dbContext.Entry(e).State = EntityState.Detached;
            return result == count;
        }

        public bool Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            int result = _dbContext.SaveChanges();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return result == 1;
        }

        public bool UpdateRange(List<TEntity> entity)
        {
            int count = entity.Count;
            _dbContext.Set<TEntity>().UpdateRange(entity);
            int result = _dbContext.SaveChanges();
            foreach (var e in entity)
                _dbContext.Entry(e).State = EntityState.Detached;
            return result == count;
        }

        public bool Delete(string id)
        {
            var entity = GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            int result = _dbContext.SaveChanges();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return result == 1;
        }

        public bool Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            int result = _dbContext.SaveChanges();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return result == 1;
        }

        public bool DeleteRange(List<TEntity> entity)
        {
            int count = entity.Count;
            _dbContext.Set<TEntity>().RemoveRange(entity);
            int result = _dbContext.SaveChanges();
            foreach (var e in entity)
                _dbContext.Entry(e).State = EntityState.Detached;
            return result == count;
        }

        public void Dispose()
        {
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}