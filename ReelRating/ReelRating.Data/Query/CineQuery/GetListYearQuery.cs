using ReelRating.Domain.Entities;
using ReelRating.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CineQuery
{
    public interface IGetListYearQuery { Task<List<int?>> GetAllYearAsync(int pageNumber, int pageSize); }

    public class GetListYearQuery : IGetListYearQuery
    {
        private readonly ICineRepository _repository;

        public GetListYearQuery(ICineRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<int?>> GetAllYearAsync(int pageNumber, int pageSize)
        {
            var result = new List<int?>();
            result = await _repository.GetCineYearsAsync(pageNumber, pageSize);
            return result;
        }
    }
}
