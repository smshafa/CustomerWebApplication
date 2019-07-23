using Customer.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLayer
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _dbSet;
        private CustomerContext _dbContext;

        public GenericRepository(CustomerContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        //public TEntity GetByName(string name)
        //{
        //    return _dbSet//Find(name);
        //}

        public int Count()
        {
            return _dbSet.Count();
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
