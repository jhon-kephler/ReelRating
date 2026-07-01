using MediatR;
using Moq;
using ReelRating.Application.Handler.Favoriteshandler.SearchHandler;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;

namespace ReelRating.Tests.Handlers.ListFavoritesByCustomerIdTests;

public class ListFavoritesByCustomerIdHandlerTests
{
    private readonly Mock<ISearchFavoritesService> _searchFavoritesServiceMock;
    private readonly ListFavoritesByCustomerIdHandler _listFavoritesByCustomerIdHandler;

    public ListFavoritesByCustomerIdHandlerTests()
    {
        _searchFavoritesServiceMock = new Mock<ISearchFavoritesService>();
        _listFavoritesByCustomerIdHandler = new ListFavoritesByCustomerIdHandler(_searchFavoritesServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginationResult_WhenFavoritesExist()
    {
        // Arrange
        var request = new ListFavoritesByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<FavoritesResponse>();
        expectedResult.SetSuccess(new List<FavoritesResponse> { new() { Id = 1, CustomerId = 1, CineId = 1 } }, 1, 10, 1);

        _searchFavoritesServiceMock
            .Setup(x => x.ListFavoritesById(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _listFavoritesByCustomerIdHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _searchFavoritesServiceMock.Verify(x => x.ListFavoritesById(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenFavoritesNotFound()
    {
        // Arrange
        var request = new ListFavoritesByCustomerIdRequest { Id = 99, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<FavoritesResponse>();
        expectedResult.ValidateResult("List Favorite with id '99' not found.", 404);

        _searchFavoritesServiceMock
            .Setup(x => x.ListFavoritesById(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _listFavoritesByCustomerIdHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _searchFavoritesServiceMock.Verify(x => x.ListFavoritesById(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new ListFavoritesByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<FavoritesResponse>();

        _searchFavoritesServiceMock
            .Setup(x => x.ListFavoritesById(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _listFavoritesByCustomerIdHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchFavoritesServiceMock.Verify(x => x.ListFavoritesById(request), Times.Once);
    }
}
