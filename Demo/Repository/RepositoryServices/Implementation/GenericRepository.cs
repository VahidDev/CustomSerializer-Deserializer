using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DomainModels.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;

namespace Repository.RepositoryServices.Implementation
{
    public class GenericRepository<T>:IGenericRepository<T> where T:class,IEntity
    {
        private readonly AppDbContext context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(AppDbContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this._logger = logger;
            dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().Where(t=>!t.IsDeleted).ToListAsync();
        }
        public async Task<int> GetCountAsync()
        {
            return await dbSet.Where(t=>!t.IsDeleted).CountAsync();
        }
        
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual async Task<bool> Exists(int id)
        {
            return await dbSet.AnyAsync(q =>!q.IsDeleted&& q.Id == id);
        }
        public virtual async Task<int> AddAsync(T entity)
        {
            try
            {
                return (await context.Set<T>().AddAsync(entity)).Entity.Id;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public virtual async Task<bool> AddRangeAsync(IList<T> entities)
        {
            try
            {
                await dbSet.AddRangeAsync(entities);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                T item = await context.Set<T>().FindAsync(id);
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual int Update(T entity)
        {
            try
            {
                return context.Set<T>().Update(entity).Entity.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.Where(t=>!t.IsDeleted).FirstOrDefaultAsync(expression);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null)
        {
            if (expression != null)
            {
                if (await dbSet.Where(t=>!t.IsDeleted).AnyAsync(expression)) return true;
            }
            if (await dbSet.Where(t => !t.IsDeleted).AnyAsync()) return true;
            return false;
        }

        public virtual async Task<IList<T>> FindAllAsync(Expression<Func<T, bool>> predicate,
            IList<string> includingItems = null)
        {
            IQueryable<T> iQuery = context.Set<T>();
            if (includingItems != null)
            {
                foreach (string item in includingItems)
                {
                    iQuery = iQuery.Include(item);
                }
            }
            return await iQuery.Where(predicate).ToListAsync();
        }
    }
}
