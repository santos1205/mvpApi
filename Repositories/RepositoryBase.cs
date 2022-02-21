using Microsoft.EntityFrameworkCore;
using mvpApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace mvpApi.Repositories
{

    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly masterContext _dbContext;

        public RepositoryBase(masterContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);

        }

        public virtual IEnumerable<TEntity> List()
        {
            return _dbContext.Set<TEntity>().AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>()
                   .Where(predicate)
                   .AsEnumerable();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            TEntity objEntity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Set<TEntity>().Remove(objEntity);
            _dbContext.SaveChanges();
        }        
    }



}
