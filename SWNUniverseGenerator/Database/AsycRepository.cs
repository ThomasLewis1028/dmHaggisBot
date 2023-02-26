using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Database
{
    public class AsyncRepository<TEntity> : IDisposable, IAsyncRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly UniverseContext _dbContext;

        public AsyncRepository(UniverseContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public async Task<TEntity> GetById(string id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public async Task Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public void Dispose()
        {
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}