using ReelRating.Domain.Entities;
using ReelRating.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CineQuery
{
    public interface IListCineByYear { Task<List<Cine>> ListCineByYearAsync(int year, int pageNumber, int pageSize); }

    public class ListCineByYearQuery : IListCineByYear
    {
        private readonly ICineRepository _repository;

        public ListCineByYearQuery(ICineRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Cine>> ListCineByYearAsync(int year, int pageNumber, int pageSize)
        {
            var result = await _repository.ListCineByYearAsync(year, pageNumber, pageSize);
            if (result == null || result.Count == 0)
            {
                throw new Exception($"No cine found for the year '{year}'.");
            }
            return result;
        }
    }
}
