using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace CastleWindsorTestApp.Models
{
    interface IRepository<TEntity>
    {
        void Create(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);
        Task Update(TEntity entity);
        Task Remove(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity FindById(int Id);
    }
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _dbContext;
        private DbSet<TEntity> _dbSet;
        #region Initialization
        public Repository(CustleWindsorContext custleWindsorContext)
        {
            _dbContext = custleWindsorContext;
            _dbSet = custleWindsorContext.Set<TEntity>();
        }
        #endregion

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }
        public void AddRange(IEnumerable<TEntity> entity)
        {
            _dbSet.AddRange(entity);
            _dbContext.SaveChanges();
        }

        public async Task Remove(int id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            _dbContext.Entry(entityToDelete).State = EntityState.Deleted;
            //_dbSet.Attach(entity);
            //_dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Detached;
            await _dbContext.SaveChangesAsync();
        }

        public TEntity FindById(int Id)
        {
            return _dbSet.Find(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        // Get All, p => p.(FK Table) Includes FK table
        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).AsNoTracking().ToList();
        }
        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }

}