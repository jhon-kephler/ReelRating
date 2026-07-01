using MediatR;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;

namespace ReelRating.Application.Handler.Favoriteshandler.SearchHandler
{
    public class SearchFavoriteByCustomerIdHandler : IRequestHandler<SearchFavoriteByCustomerIdRequest, Result<FavoritesResponse>>
    {
        private readonly ISearchFavoritesService _searchFavoritesService;

        public SearchFavoriteByCustomerIdHandler(ISearchFavoritesService searchFavoritesService)
        {
            _searchFavoritesService = searchFavoritesService;
        }

        public async Task<Result<FavoritesResponse>> Handle(SearchFavoriteByCustomerIdRequest request, CancellationToken cancellationToken) =>
                                await _searchFavoritesService.SearchFavoriteById(request);
    }
}
