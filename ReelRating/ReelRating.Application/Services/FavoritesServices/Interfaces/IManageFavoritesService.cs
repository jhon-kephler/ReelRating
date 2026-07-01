using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.FavoritesServices.Interfaces
{
    public interface IManageFavoritesService
    {
        Task<Result> CreateFavorite(CreateFavoriteRequest request);
        Task<Result> DeleteFavorite(DeleteFavoriteRequest request);
    }
}
