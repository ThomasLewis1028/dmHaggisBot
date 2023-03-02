using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Database
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        IEnumerable<TEntity> Search(Expression<System.Func<TEntity, Boolean>> query);

        Boolean Any(Expression<System.Func<TEntity, Boolean>> query);

        Int32 Count(Expression<System.Func<TEntity, Boolean>> query);
        
        TEntity GetById(string id);

        bool Add(TEntity entity);
        
        bool AddRange(List<TEntity> entity);

        bool Update(TEntity entity);
        
        bool UpdateRange(List<TEntity> entity);

        bool Delete(string id);
        
        bool Delete(TEntity entity);
        
        bool DeleteRange(List<TEntity> entity);
    }
}