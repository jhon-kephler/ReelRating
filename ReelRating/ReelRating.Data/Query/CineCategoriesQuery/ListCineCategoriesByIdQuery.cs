using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CineCategoriesQuery
{
    public interface IListCineCategoriesByIdQuery { Task<List<CineCategories>> ListCineCategoriesByIdAsync(int id); }

    public class ListCineCategoriesByIdQuery : IListCineCategoriesByIdQuery
    {
        private readonly IRepository<CineCategories> _repository;

        public ListCineCategoriesByIdQuery(IRepository<CineCategories> repository)
        {
            _repository = repository;
        }

        public async Task<List<CineCategories>> ListCineCategoriesByIdAsync(int id)
        {
            var result = await _repository.ListAllByIdPaginationAsync(x => x.CategoriesId == id);

            if (result == null || result.Count() == 0)
                throw new Exception($"No cine categories found for the id '{id}'.");

            return result.ToList();
        }
    }
}
