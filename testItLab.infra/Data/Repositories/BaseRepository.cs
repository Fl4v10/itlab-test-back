using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using testeItLab.domain.Interfaces;
using testItLab.infra.Data.Context;

namespace testItLab.infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly TestItLabDbContext _dbContext;

        public BaseRepository(TestItLabDbContext context)
        {
            _dbContext = context;
        }
        public virtual void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Set<TEntity>().Update(entity);

            await _dbContext.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChangesAsync();
        }
    }
}