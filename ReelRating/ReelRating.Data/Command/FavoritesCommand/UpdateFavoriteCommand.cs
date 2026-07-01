using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Command.FavoritesCommand
{
    public interface IUpdateFavoriteCommand { Task UpdateFavorite(int id, Favorites favorites); }

    public class UpdateFavoriteCommand : IUpdateFavoriteCommand
    {
        private readonly IRepository<Favorites> _repository;

        public UpdateFavoriteCommand(IRepository<Favorites> repository)
        {
            _repository = repository;
        }

        public Task UpdateFavorite(int id, Favorites favorites)
        {
            _repository.Update(id, favorites);
            return Task.CompletedTask;
        }
    }
}
