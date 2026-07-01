using MediatR;
using Moq;
using ReelRating.Application.Handler.Favoriteshandler.ManageHandler;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;

namespace ReelRating.Tests.Handlers.DeleteFavoritesTests;

public class DeleteFavoritesHandlerTests
{
    private readonly Mock<IManageFavoritesService> _manageFavoritesServiceMock;
    private readonly DeleteFavoritesHandler _deleteFavoritesHandler;

    public DeleteFavoritesHandlerTests()
    {
        _manageFavoritesServiceMock = new Mock<IManageFavoritesService>();
        _deleteFavoritesHandler = new DeleteFavoritesHandler(_manageFavoritesServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFavoriteDeletedSuccessfully()
    {
        // Arrange
        var request = new DeleteFavoriteRequest { Id = 1 };
        var expectedResult = new Result();
        expectedResult.SetSuccess(true);

        _manageFavoritesServiceMock
            .Setup(x => x.DeleteFavorite(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _deleteFavoritesHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _manageFavoritesServiceMock.Verify(x => x.DeleteFavorite(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenFavoriteNotFound()
    {
        // Arrange
        var request = new DeleteFavoriteRequest { Id = 99 };
        var expectedResult = new Result();
        expectedResult.ValidateResult("Review with id '99' not found.", 404);

        _manageFavoritesServiceMock
            .Setup(x => x.DeleteFavorite(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _deleteFavoritesHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.StatusCode);
        _manageFavoritesServiceMock.Verify(x => x.DeleteFavorite(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new DeleteFavoriteRequest { Id = 1 };
        var expectedResult = new Result();

        _manageFavoritesServiceMock
            .Setup(x => x.DeleteFavorite(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _deleteFavoritesHandler.Handle(request, CancellationToken.None);

        // Assert
        _manageFavoritesServiceMock.Verify(x => x.DeleteFavorite(request), Times.Once);
    }
}
