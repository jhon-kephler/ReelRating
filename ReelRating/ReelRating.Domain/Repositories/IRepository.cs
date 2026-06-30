using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ReelRating.Domain.Repository
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(int id);
        IEnumerable<T> GetAll();
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAllPagination(int pageNumber, int pageSize);
        IEnumerable<T> GetAllByPredicatePagination(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);
        Task<IEnumerable<T>> ListAllByIdPaginationAsync(Expression<Func<T, bool>> predicate);
    }
}
