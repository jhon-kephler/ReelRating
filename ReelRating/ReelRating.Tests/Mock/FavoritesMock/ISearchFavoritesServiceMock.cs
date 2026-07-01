using Moq;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;

namespace ReelRating.Tests.Mock.FavoritesMock;

public static class ISearchFavoritesServiceMock
{
    public static Mock<ISearchFavoritesService> Create()
    {
        return new Mock<ISearchFavoritesService>();
    }

    public static Mock<ISearchFavoritesService> SetupSearchFavoriteById(this Mock<ISearchFavoritesService> mock, SearchFavoriteByCustomerIdRequest request, Result<FavoritesResponse> result)
    {
        mock.Setup(x => x.SearchFavoriteById(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<ISearchFavoritesService> SetupListFavorites(this Mock<ISearchFavoritesService> mock, ListFavoritesRequest request, PaginationResult<FavoritesResponse> result)
    {
        mock.Setup(x => x.ListFavorites(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<ISearchFavoritesService> SetupListFavoritesById(this Mock<ISearchFavoritesService> mock, ListFavoritesByCustomerIdRequest request, PaginationResult<FavoritesResponse> result)
    {
        mock.Setup(x => x.ListFavoritesById(request)).ReturnsAsync(result);
        return mock;
    }
}
