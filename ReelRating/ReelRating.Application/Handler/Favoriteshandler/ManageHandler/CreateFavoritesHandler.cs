using MediatR;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.Favoriteshandler.ManageHandler
{
    public class CreateFavoritesHandler : IRequestHandler<CreateFavoriteRequest, Result>
    {
        private readonly IManageFavoritesService _manageFavoritesService;

        public CreateFavoritesHandler(IManageFavoritesService manageFavoritesService)
        {
            _manageFavoritesService = manageFavoritesService;
        }

        public async Task<Result> Handle(CreateFavoriteRequest request, CancellationToken cancellationToken) =>
                                await _manageFavoritesService.CreateFavorite(request);
    }
}
