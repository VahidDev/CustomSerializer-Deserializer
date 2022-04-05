using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DomainModels.Entities.Base;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
        Task<bool> AddRangeAsync(IList<T> entities);
        Task<bool> DeleteAsync(int id);
        Task<bool> Exists(int id);
        int Update(T entity);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null);
        Task<int> GetCountAsync();

        Task<IList<T>> FindAllAsync
            (Expression<Func<T, bool>> predicate, IList<string> includingItems = null);
    }
}
