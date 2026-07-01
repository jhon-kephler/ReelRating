using MediatR;
using Moq;
using ReelRating.Application.Handler.Favoriteshandler.SearchHandler;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;

namespace ReelRating.Tests.Handlers.ListFavoritesTests;

public class ListFavoritesHandlerTests
{
    private readonly Mock<ISearchFavoritesService> _searchFavoritesServiceMock;
    private readonly ListFavoritesHandler _listFavoritesHandler;

    public ListFavoritesHandlerTests()
    {
        _searchFavoritesServiceMock = new Mock<ISearchFavoritesService>();
        _listFavoritesHandler = new ListFavoritesHandler(_searchFavoritesServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginationResult_WhenFavoritesExist()
    {
        // Arrange
        var request = new ListFavoritesRequest { PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<FavoritesResponse>();
        expectedResult.SetSuccess(new List<FavoritesResponse> { new() { Id = 1 }, new() { Id = 2 } }, 1, 10, 2);

        _searchFavoritesServiceMock
            .Setup(x => x.ListFavorites(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _listFavoritesHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _searchFavoritesServiceMock.Verify(x => x.ListFavorites(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new ListFavoritesRequest { PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<FavoritesResponse>();

        _searchFavoritesServiceMock
            .Setup(x => x.ListFavorites(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _listFavoritesHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchFavoritesServiceMock.Verify(x => x.ListFavorites(request), Times.Once);
    }
}
