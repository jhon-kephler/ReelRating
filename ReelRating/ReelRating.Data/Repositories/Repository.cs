using Microsoft.EntityFrameworkCore;
using SelectedMovie.Domain.Repository;

namespace SelectedMovie.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        protected DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.SaveChanges();
        }

        public void Update(int id, T entity)
        {
            var existingEntity = _dbSet.Find(id);
            if (existingEntity != null)
            {
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    var newValue = property.GetValue(entity);
                    var oldValue = property.GetValue(existingEntity);
                    var propertyName = property.Name; // Obtém o nome da propriedade

                    // Verifica se o novo valor não é nulo e é diferente do valor existente
                    if (newValue != null && !newValue.Equals(oldValue))
                    {
                        // Verifica se o novo valor não é zero (para tipos numéricos)
                        if (!IsZero(newValue))
                        {
                            property.SetValue(existingEntity, newValue);
                            _dbContext.Entry(existingEntity).Property(propertyName).IsModified = true;
                        }
                    }
                }
                _dbContext.SaveChanges();
            }
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

        private bool IsZero(object value)
        {
            if (value is int intValue)
            {
                return intValue == 0;
            }
            else if (value is double doubleValue)
            {
                return doubleValue == 0.0;
            }

            return false;
        }
    }
}