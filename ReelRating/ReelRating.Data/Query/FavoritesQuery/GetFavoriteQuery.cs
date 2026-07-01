using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.FavoritesQuery
{
    public interface IGetFavoriteQuery
    {
        Task<Favorites?> GetFavoriteById(int id);
        Task<Favorites?> GetFavoriteByIdAndCustomerId(int id, int customerId);
        Task<Favorites?> GetFavoriteByCustomerIdAndCineId(int customerId, int cineId);
        Task<Favorites?> GetFavoriteByIdAndCustomerIdAndCineId(int id, int customerId, int cineId);
    }

    public class GetFavoriteQuery : IGetFavoriteQuery
    {
        private readonly IRepository<Favorites> _repository;

        public GetFavoriteQuery(IRepository<Favorites> repository)
        {
            _repository = repository;
        }
        public async Task<Favorites?> GetFavoriteById(int id)
        {
            var result = new Favorites();
            result = await _repository.GetAsync(x => x.Id == id);
            return result;
        }

        public async Task<Favorites?> GetFavoriteByIdAndCustomerId(int id, int customerId)
        {
            var result = new Favorites();
            result = await _repository.GetAsync(x => x.Id == id && x.CustomerId == customerId);
            return result;
        }
        public async Task<Favorites?> GetFavoriteByCustomerIdAndCineId(int customerId, int cineId)
        {
            var result = new Favorites();
            result = await _repository.GetAsync(x => x.CustomerId == customerId && x.CineId == cineId);
            return result;
        }

        public async Task<Favorites?> GetFavoriteByIdAndCustomerIdAndCineId(int id, int customerId, int cineId)
        {
            var result = new Favorites();
            result = await _repository.GetAsync(x => x.Id == id && x.CustomerId == customerId && x.CineId == cineId);
            return result;
        }
    }
}
