using ReelRating.Domain.Entities;
using ReelRating.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CineQuery
{
    public interface IGetCineByIdQuery { Task<Cine> GetCineByIdAsync(int cineId); }

    public class GetCineByIdQuery : IGetCineByIdQuery
    {
        private readonly ICineRepository _repository;

        public GetCineByIdQuery(ICineRepository repository)
        {
            _repository = repository;
        }

        public async Task<Cine?> GetCineByIdAsync(int cineId)
        {
            var result = await _repository.GetCineByIdAsync(cineId);
            if (result == null)
            {
                throw new Exception($"Cine with ID '{cineId}' not found.");
            }
            return result;
        }
    }
}
