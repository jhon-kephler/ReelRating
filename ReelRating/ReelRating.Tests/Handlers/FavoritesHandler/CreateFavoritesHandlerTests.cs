using MediatR;
using Moq;
using ReelRating.Application.Handler.Favoriteshandler.ManageHandler;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;

namespace ReelRating.Tests.Handlers.CreateFavoritesTests;

public class CreateFavoritesHandlerTests
{
    private readonly Mock<IManageFavoritesService> _manageFavoritesServiceMock;
    private readonly CreateFavoritesHandler _createFavoritesHandler;

    public CreateFavoritesHandlerTests()
    {
        _manageFavoritesServiceMock = new Mock<IManageFavoritesService>();
        _createFavoritesHandler = new CreateFavoritesHandler(_manageFavoritesServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFavoriteCreatedSuccessfully()
    {
        // Arrange
        var request = new CreateFavoriteRequest { CustomerId = 1, CineId = 1 };
        var expectedResult = new Result();
        expectedResult.SetSuccess(true);

        _manageFavoritesServiceMock
            .Setup(x => x.CreateFavorite(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _createFavoritesHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _manageFavoritesServiceMock.Verify(x => x.CreateFavorite(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenFavoriteCreationFails()
    {
        // Arrange
        var request = new CreateFavoriteRequest { CustomerId = 1, CineId = 1 };
        var expectedResult = new Result();
        expectedResult.ValidateResult("An error occurred while fetching categories: Database error");

        _manageFavoritesServiceMock
            .Setup(x => x.CreateFavorite(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _createFavoritesHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _manageFavoritesServiceMock.Verify(x => x.CreateFavorite(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new CreateFavoriteRequest { CustomerId = 1, CineId = 1 };
        var expectedResult = new Result();

        _manageFavoritesServiceMock
            .Setup(x => x.CreateFavorite(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _createFavoritesHandler.Handle(request, CancellationToken.None);

        // Assert
        _manageFavoritesServiceMock.Verify(x => x.CreateFavorite(request), Times.Once);
    }
}
