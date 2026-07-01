using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.FavoritesServices.Interfaces
{
    public interface ISearchFavoritesService
    {
        Task<Result<FavoritesResponse>> SearchFavoriteById(SearchFavoriteByCustomerIdRequest request);
        Task<Result<FavoritesResponse>> SearchFavoriteByIdAndCineId(SearchFavoriteByCustomerIdAndCineIdRequest request);
        Task<PaginationResult<FavoritesResponse>> ListFavorites(ListFavoritesRequest request);
        Task<PaginationResult<FavoritesResponse>> ListFavoritesById(ListFavoritesByCustomerIdRequest request);
    }
}
