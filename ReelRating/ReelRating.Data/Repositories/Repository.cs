using Microsoft.EntityFrameworkCore;
using ReelRating.Domain.Repository;
using System.Linq.Expressions;

namespace ReelRating.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        protected DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(int id, T entity)
        {
            var existingEntity = _dbSet.Find(id);
            if (existingEntity == null) return;

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("A entidade com o ID fornecido não foi encontrado.");
            }
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public IEnumerable<T> GetAllPagination(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<IEnumerable<T>> ListAllByIdPaginationAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}