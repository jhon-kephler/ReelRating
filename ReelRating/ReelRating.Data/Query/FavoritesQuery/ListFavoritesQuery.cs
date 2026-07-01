using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.FavoritesQuery
{
    public interface IListFavoritesQuery
    {
        Task<List<Favorites?>> ListFavorites(int pageNumber, int pageSize);
        Task<List<Favorites?>> ListFavoritesById(int id, int pageNumber, int pageSize);
    }

    public class ListFavoritesQuery : IListFavoritesQuery
    {
        private readonly IRepository<Favorites> _repository;

        public ListFavoritesQuery(IRepository<Favorites> repository)
        {
            _repository = repository;
        }

        public async Task<List<Favorites?>> ListFavoritesById(int id, int pageNumber, int pageSize)
        {
            var result = new List<Favorites?>();
            result = _repository.GetAllByPredicatePagination(x => x.CustomerId == id && x.Deleted == false, pageNumber, pageSize).ToList();
            return result;
        }

        public async Task<List<Favorites?>> ListFavorites(int pageNumber, int pageSize)
        {
            var result = new List<Favorites?>();
            result = _repository.GetAllByPredicatePagination(x => x.Deleted == false, pageNumber, pageSize).ToList();
            return result;
        }
    }
}
