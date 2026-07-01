using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Command.FavoritesCommand
{
    public interface ICreateFavoriteCommand { Task AddFavorite(Favorites request); }

    public class CreateFavoriteCommand : ICreateFavoriteCommand
    {
        private readonly IRepository<Favorites> _repository;

        public CreateFavoriteCommand(IRepository<Favorites> repository)
        {
            _repository = repository;
        }

        public async Task AddFavorite(Favorites request)
        {
            _repository.Add(request);
        }
    }
}
