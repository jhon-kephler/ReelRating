using ReelRating.Domain.Entities;
using ReelRating.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CineQuery
{
    public interface IGetCineByNameQuery { Task<Cine> GetCineByNameAsync(string cine); }

    public class GetCineByNameQuery : IGetCineByNameQuery
    {
        private readonly ICineRepository _repository;

        public GetCineByNameQuery(ICineRepository repository)
        {
            _repository = repository;
        }

        public async Task<Cine?> GetCineByNameAsync(string cine)
        {
            var result = await _repository.GetCineByNameAsync(cine);
            if (result == null)
            {
                throw new Exception($"Cine with name '{cine}' not found.");
            }

            return result;
        }
    }
}
