using RepositoryPatternWithUOW.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
       
          
        T GetOne(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<T> GetOneAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll( string[] includes);
        Task<IEnumerable<T>> GetAllAsync(string[] includes);
        IEnumerable<T> GetAllWithCriteria(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> GetAllWithCriteriaAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, string[] includes = null);
  
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, string[] includes = null);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
        T Add(T entity);
        void AddRange(IEnumerable<T> entities);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

    }
}
