using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;

namespace ReelRating.Data.Query.FiltersQuery
{
    public interface IListCategories { Task<List<Categories>> GetAllAsync(int pageNumber, int pageSize); }

    public class ListCategoriesQuery : IListCategories
    {
        private IRepository<Categories> _repository;

        public ListCategoriesQuery(IRepository<Categories> repository)
        {
            _repository = repository;
        }

        public async Task<List<Categories>> GetAllAsync(int pageNumber, int pageSize)
        {
            var result = new List<Categories>();
            result = _repository.GetAllPagination(pageNumber, pageSize).ToList();
            return result;
        }
    }
}
