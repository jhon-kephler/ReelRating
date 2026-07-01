using MediatR;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.Favoriteshandler.ManageHandler
{
    public class DeleteFavoritesHandler : IRequestHandler<DeleteFavoriteRequest, Result>
    {
        private readonly IManageFavoritesService _manageFavoritesService;

        public DeleteFavoritesHandler(IManageFavoritesService manageFavoritesService)
        {
            _manageFavoritesService = manageFavoritesService;
        }

        public async Task<Result> Handle(DeleteFavoriteRequest request, CancellationToken cancellationToken) =>
                                await _manageFavoritesService.DeleteFavorite(request);
    }
}
