using MediatR;
using Moq;
using ReelRating.Application.Handler.Favoriteshandler.SearchHandler;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;

namespace ReelRating.Tests.Handlers.SearchFavoriteByCustomerIdAndCineIdTests;

public class SearchFavoriteByCustomerIdAndCineIdHandlerTests
{
    private readonly Mock<ISearchFavoritesService> _searchFavoritesServiceMock;
    private readonly SearchFavoriteByCustomerIdAndCineIdHandler _searchFavoriteByCustomerIdAndCineIdHandler;

    public SearchFavoriteByCustomerIdAndCineIdHandlerTests()
    {
        _searchFavoritesServiceMock = new Mock<ISearchFavoritesService>();
        _searchFavoriteByCustomerIdAndCineIdHandler = new SearchFavoriteByCustomerIdAndCineIdHandler(_searchFavoritesServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFavoriteFound()
    {
        // Arrange
        var request = new SearchFavoriteByCustomerIdAndCineIdRequest { Id = 1, CustomerId = 1, CineId = 1 };
        var expectedResult = new Result<FavoritesResponse>();
        expectedResult.SetSuccess(new FavoritesResponse { Id = 1, CustomerId = 1, CineId = 1 });

        _searchFavoritesServiceMock
            .Setup(x => x.SearchFavoriteByIdAndCineId(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchFavoriteByCustomerIdAndCineIdHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        _searchFavoritesServiceMock.Verify(x => x.SearchFavoriteByIdAndCineId(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenFavoriteNotFound()
    {
        // Arrange
        var request = new SearchFavoriteByCustomerIdAndCineIdRequest { Id = 99, CustomerId = 1, CineId = 1 };
        var expectedResult = new Result<FavoritesResponse>();
        expectedResult.ValidateResult("Favorite with id '99' not found.", 404);

        _searchFavoritesServiceMock
            .Setup(x => x.SearchFavoriteByIdAndCineId(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchFavoriteByCustomerIdAndCineIdHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _searchFavoritesServiceMock.Verify(x => x.SearchFavoriteByIdAndCineId(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new SearchFavoriteByCustomerIdAndCineIdRequest { Id = 1, CustomerId = 1, CineId = 1 };
        var expectedResult = new Result<FavoritesResponse>();

        _searchFavoritesServiceMock
            .Setup(x => x.SearchFavoriteByIdAndCineId(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _searchFavoriteByCustomerIdAndCineIdHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchFavoritesServiceMock.Verify(x => x.SearchFavoriteByIdAndCineId(request), Times.Once);
    }
}
