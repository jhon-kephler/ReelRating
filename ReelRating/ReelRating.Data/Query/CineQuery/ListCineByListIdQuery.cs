using ReelRating.Domain.Entities;
using ReelRating.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CineQuery
{
    public interface IListCineByListIdQuery { Task<List<Cine>> ListCineByListIdAsync(List<int> ids); }

    public class ListCineByListIdQuery : IListCineByListIdQuery
    {
        private readonly ICineRepository _cineRepository;

        public ListCineByListIdQuery(ICineRepository cineRepository)
        {
            _cineRepository = cineRepository;
        }

        public async Task<List<Cine>> ListCineByListIdAsync(List<int> ids)
        {
            var result = await _cineRepository.ListCineByIdsAsync(ids);
            if (result == null || result.Count == 0)
            {
                throw new Exception($"No cine found for the provided list of IDs.");
            }
            return result;
        }
    }
}
