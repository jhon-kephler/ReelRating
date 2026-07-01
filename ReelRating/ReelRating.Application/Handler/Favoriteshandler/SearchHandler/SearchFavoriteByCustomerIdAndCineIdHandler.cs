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
    public class SearchFavoriteByCustomerIdAndCineIdHandler : IRequestHandler<SearchFavoriteByCustomerIdAndCineIdRequest, Result<FavoritesResponse>>
    {
        private readonly ISearchFavoritesService _searchFavoritesService;

        public SearchFavoriteByCustomerIdAndCineIdHandler(ISearchFavoritesService searchFavoritesService)
        {
            _searchFavoritesService = searchFavoritesService;
        }

        public async Task<Result<FavoritesResponse>> Handle(SearchFavoriteByCustomerIdAndCineIdRequest request, CancellationToken cancellationToken) =>
                                await _searchFavoritesService.SearchFavoriteByIdAndCineId(request);
    
    }
}
