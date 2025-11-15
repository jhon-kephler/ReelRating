using System;
using System.Collections.Generic;
using System.Text;

namespace SelectedMovie.Domain.Repository
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(int id);
        IEnumerable<T> GetAll();
    }
}
