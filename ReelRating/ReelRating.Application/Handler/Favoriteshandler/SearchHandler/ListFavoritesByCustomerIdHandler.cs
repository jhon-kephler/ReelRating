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
    public class ListFavoritesByCustomerIdHandler : IRequestHandler<ListFavoritesByCustomerIdRequest, PaginationResult<FavoritesResponse>>
    {
        private readonly ISearchFavoritesService _searchFavoritesService;

        public ListFavoritesByCustomerIdHandler(ISearchFavoritesService searchFavoritesService)
        {
            _searchFavoritesService = searchFavoritesService;
        }

        public async Task<PaginationResult<FavoritesResponse>> Handle(ListFavoritesByCustomerIdRequest request, CancellationToken cancellationToken) =>
                                await _searchFavoritesService.ListFavoritesById(request);
    }
}
