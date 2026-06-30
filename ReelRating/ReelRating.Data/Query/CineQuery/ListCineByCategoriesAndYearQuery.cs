using ReelRating.Domain.Entities;
using ReelRating.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CineQuery
{
    public interface IListCineByCategoriesAndYearQuery { Task<(List<Cine> Items, int Total)> ListCineByCategoriesAndYearAsync(int? categoriesId, int? year, int pageNumber, int pageSize); }

    public class ListCineByCategoriesAndYearQuery : IListCineByCategoriesAndYearQuery
    {
        private readonly ICineRepository _repository;

        public ListCineByCategoriesAndYearQuery(ICineRepository repository)
        {
            _repository = repository;
        }

        public async Task<(List<Cine> Items, int Total)> ListCineByCategoriesAndYearAsync(int? categoriesId, int? year, int pageNumber, int pageSize)
        {
            var result = await _repository.SearchCineAsync(categoriesId, year, pageNumber, pageSize);

            if (result.Items == null || result.Items.Count == 0)
            {
                throw new Exception($"No cine found for the categories '{categoriesId}' and year '{year}'.");
            }
            return result;
        }
    }
}
