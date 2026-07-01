using Moq;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;

namespace ReelRating.Tests.Mock.FavoritesMock;

public static class IManageFavoritesServiceMock
{
    public static Mock<IManageFavoritesService> Create()
    {
        return new Mock<IManageFavoritesService>();
    }

    public static Mock<IManageFavoritesService> SetupCreateFavorite(this Mock<IManageFavoritesService> mock, CreateFavoriteRequest request, Result result)
    {
        mock.Setup(x => x.CreateFavorite(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageFavoritesService> SetupCreateFavoriteSuccess(this Mock<IManageFavoritesService> mock, CreateFavoriteRequest request)
    {
        var result = new Result();
        result.SetSuccess(true);
        mock.Setup(x => x.CreateFavorite(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageFavoritesService> SetupDeleteFavorite(this Mock<IManageFavoritesService> mock, DeleteFavoriteRequest request, Result result)
    {
        mock.Setup(x => x.DeleteFavorite(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageFavoritesService> SetupDeleteFavoriteSuccess(this Mock<IManageFavoritesService> mock, DeleteFavoriteRequest request)
    {
        var result = new Result();
        result.SetSuccess(true);
        mock.Setup(x => x.DeleteFavorite(request)).ReturnsAsync(result);
        return mock;
    }
}
