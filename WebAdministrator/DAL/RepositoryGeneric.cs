using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebAdministrator.DAL
{
    public class RepositoryGeneric<TEntity> : IDisposable, IOperation<TEntity> where TEntity : class
    {
        private bool disposed = false;
        private DbCon dbContext;
        private DbSet<TEntity> dbSet;

        public RepositoryGeneric(DbCon dataContext, out GenericRepositoryValidation.GenericRepositoryExceptionStatus status)
        {
            status = GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success;
            if (dataContext == null) status = GenericRepositoryValidation.GenericRepositoryExceptionStatus.ArgumentNullException;

            dbContext = dataContext;
            dbSet = dbContext.Set<TEntity>();
        }

        ~RepositoryGeneric() { Dispose(false); }

        public int Create(TEntity entity)
        {
            try
            {
                dbSet.Add(entity);
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int Delete(TEntity Entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        { 
            if(!this.disposed)
            {
                if (disposing)
                {
                    if(dbContext != null)
                    {
                        dbContext.Dispose();
                        dbContext = null;
                    }
                }
            }

            disposed = true;
        }

        public int Edit(TEntity entity)
        {
            try
            {
                dbSet.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            try
            {
                return dbSet.Where(predicate).AsNoTracking();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity GetItem(Expression<Func<TEntity, bool>> predicate = null)
        {
            try
            {
                return dbSet.FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
