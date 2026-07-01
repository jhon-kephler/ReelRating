using MediatR;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.Favoriteshandler.SearchHandler
{
    public class ListFavoritesHandler : IRequestHandler<ListFavoritesRequest, PaginationResult<FavoritesResponse>>
    {
        private readonly ISearchFavoritesService _searchFavoritesService;

        public ListFavoritesHandler(ISearchFavoritesService searchFavoritesService)
        {
            _searchFavoritesService = searchFavoritesService;
        }

        public async Task<PaginationResult<FavoritesResponse>> Handle(ListFavoritesRequest request, CancellationToken cancellationToken) =>
                                await _searchFavoritesService.ListFavorites(request);
    }
}
