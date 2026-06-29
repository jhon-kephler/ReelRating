using ReelRating.Domain.Entities;
using ReelRating.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CineQuery
{
    public interface IListCineByCategories { }

    public class ListCineByCategoriesQuery : IListCineByCategories
    {
        private readonly ICineRepository _repository;

        public ListCineByCategoriesQuery(ICineRepository repository)
        {
            _repository = repository;
        }

        //public async Task<List<Cine>> GetCineByCategoriesAsync(string category, int pageNumber, int pageSize)
        //{
        //    //var result = await _repository.GetCineByCategoriesAsync(category, pageNumber, pageSize);
        //    if (result == null || result.Count == 0)
        //    {
        //        throw new Exception($"No cine found for the category '{category}'.");
        //    }
        //    return result;
        //}
    }
}
