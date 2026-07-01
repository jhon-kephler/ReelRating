using AutoMapper;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;
using ReelRating.Data.Query.FavoritesQuery;

namespace ReelRating.Application.Services.FavoritesServices
{
    public class SearchFavoritesService : ISearchFavoritesService
    {
        private readonly IMapper _mapper;
        private readonly IListFavoritesQuery _listFavoritesQuery;
        private readonly IGetFavoriteQuery _getFavoriteQuery;

        public SearchFavoritesService(IMapper mapper, IListFavoritesQuery listFavoritesQuery, IGetFavoriteQuery getFavoriteQuery)
        {
            _mapper = mapper;
            _listFavoritesQuery = listFavoritesQuery;
            _getFavoriteQuery = getFavoriteQuery;
        }

        public async Task<Result<FavoritesResponse>> SearchFavoriteById(SearchFavoriteByCustomerIdRequest request)
        {
            var result = new Result<FavoritesResponse>();
            try
            {
                var Favorite = await _getFavoriteQuery.GetFavoriteByIdAndCustomerId(request.Id, request.CustomerId);
                if (Favorite == null)
                {
                    result.ValidateResult($"Favorite with id '{request.Id}' not found.", 404);
                    return result;
                }
                result.SetSuccess(_mapper.Map<FavoritesResponse>(Favorite));
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }

        public async Task<Result<FavoritesResponse>> SearchFavoriteByIdAndCineId(SearchFavoriteByCustomerIdAndCineIdRequest request)
        {
            var result = new Result<FavoritesResponse>();
            try
            {
                var Favorite = await _getFavoriteQuery.GetFavoriteByIdAndCustomerIdAndCineId(request.Id, request.CustomerId, request.CineId);
                if (Favorite == null)
                {
                    result.ValidateResult($"Favorite with id '{request.Id}' not found.", 404);
                    return result;
                }
                result.SetSuccess(_mapper.Map<FavoritesResponse>(Favorite));
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }

        public async Task<PaginationResult<FavoritesResponse>> ListFavorites(ListFavoritesRequest request)
        {
            var result = new PaginationResult<FavoritesResponse>();
            try
            {
                var Favorites = await _listFavoritesQuery.ListFavorites(request.PageNumber, request.PageSize);

                result.SetSuccess(_mapper.Map<List<FavoritesResponse>>(Favorites), request.PageNumber, request.PageSize, Favorites.Count);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }

        public async Task<PaginationResult<FavoritesResponse>> ListFavoritesById(ListFavoritesByCustomerIdRequest request)
        {
            var result = new PaginationResult<FavoritesResponse>();
            try
            {
                var Favorites = await _listFavoritesQuery.ListFavoritesById(request.Id, request.PageNumber, request.PageSize);
                if (Favorites == null)
                {
                    result.ValidateResult($"List Favorite with id '{request.Id}' not found.", 404);
                    return result;
                }

                result.SetSuccess(_mapper.Map<List<FavoritesResponse>>(Favorites), request.PageNumber, request.PageSize, Favorites.Count);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }
    }
}
