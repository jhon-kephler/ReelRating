using AutoMapper;
using Moq;
using ReelRating.Application.Services.FavoritesServices;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Data.Command.FavoritesCommand;
using ReelRating.Data.Query.FavoritesQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Tests.Services.ManageFavoritesTests;

public class ManageFavoritesServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICreateFavoriteCommand> _createFavoriteCommandMock;
    private readonly Mock<IUpdateFavoriteCommand> _updateFavoriteCommandMock;
    private readonly Mock<IGetFavoriteQuery> _getFavoriteQueryMock;
    private readonly ManageFavoritesService _manageFavoritesService;

    public ManageFavoritesServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _createFavoriteCommandMock = new Mock<ICreateFavoriteCommand>();
        _updateFavoriteCommandMock = new Mock<IUpdateFavoriteCommand>();
        _getFavoriteQueryMock = new Mock<IGetFavoriteQuery>();

        _manageFavoritesService = new ManageFavoritesService(
            _mapperMock.Object,
            _createFavoriteCommandMock.Object,
            _updateFavoriteCommandMock.Object,
            _getFavoriteQueryMock.Object
        );
    }

    [Fact]
    public async Task CreateFavorite_ShouldCreateNew_WhenFavoriteDoesNotExist()
    {
        // Arrange
        var request = new CreateFavoriteRequest { CustomerId = 1, CineId = 1 };
        var favoriteEntity = new Favorites { Id = 1, CustomerId = 1, CineId = 1 };

        _getFavoriteQueryMock
            .Setup(x => x.GetFavoriteByCustomerIdAndCineId(request.CustomerId, request.CineId))
            .ReturnsAsync((Favorites?)null);

        _mapperMock.Setup(x => x.Map<Favorites>(request)).Returns(favoriteEntity);
        _createFavoriteCommandMock.Setup(x => x.AddFavorite(favoriteEntity)).Returns(Task.CompletedTask);

        // Act
        var result = await _manageFavoritesService.CreateFavorite(request);

        // Assert
        Assert.True(result.IsSuccess);
        _getFavoriteQueryMock.Verify(x => x.GetFavoriteByCustomerIdAndCineId(request.CustomerId, request.CineId), Times.Once);
        _createFavoriteCommandMock.Verify(x => x.AddFavorite(favoriteEntity), Times.Once);
        _updateFavoriteCommandMock.Verify(x => x.UpdateFavorite(It.IsAny<int>(), It.IsAny<Favorites>()), Times.Never);
    }

    [Fact]
    public async Task CreateFavorite_ShouldUpdateExisting_WhenFavoriteExists()
    {
        // Arrange
        var request = new CreateFavoriteRequest { CustomerId = 1, CineId = 1 };
        var existingFavorite = new Favorites { Id = 1, CustomerId = 1, CineId = 1, Deleted = true };
        var favoriteEntity = new Favorites { Id = 1, CustomerId = 1, CineId = 1, Deleted = false };

        _getFavoriteQueryMock
            .Setup(x => x.GetFavoriteByCustomerIdAndCineId(request.CustomerId, request.CineId))
            .ReturnsAsync(existingFavorite);

        _mapperMock.Setup(x => x.Map<Favorites>(request)).Returns(favoriteEntity);
        _updateFavoriteCommandMock.Setup(x => x.UpdateFavorite(existingFavorite.Id, It.IsAny<Favorites>())).Returns(Task.CompletedTask);

        // Act
        var result = await _manageFavoritesService.CreateFavorite(request);

        // Assert
        Assert.True(result.IsSuccess);
        _getFavoriteQueryMock.Verify(x => x.GetFavoriteByCustomerIdAndCineId(request.CustomerId, request.CineId), Times.Once);
        _updateFavoriteCommandMock.Verify(x => x.UpdateFavorite(existingFavorite.Id, It.Is<Favorites>(f => f.Deleted == false)), Times.Once);
        _createFavoriteCommandMock.Verify(x => x.AddFavorite(It.IsAny<Favorites>()), Times.Never);
    }

    [Fact]
    public async Task CreateFavorite_ShouldReturnError_WhenExceptionThrown()
    {
        // Arrange
        var request = new CreateFavoriteRequest { CustomerId = 1, CineId = 1 };
        _getFavoriteQueryMock
            .Setup(x => x.GetFavoriteByCustomerIdAndCineId(request.CustomerId, request.CineId))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _manageFavoritesService.CreateFavorite(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("error", result.ErrorMessage.ToLower());
    }

    [Fact]
    public async Task DeleteFavorite_ShouldReturnSuccess_WhenFavoriteExists()
    {
        // Arrange
        var request = new DeleteFavoriteRequest { Id = 1 };
        var favorite = new Favorites { Id = 1, CustomerId = 1, CineId = 1, Deleted = false };

        _getFavoriteQueryMock.Setup(x => x.GetFavoriteById(request.Id)).ReturnsAsync(favorite);
        _updateFavoriteCommandMock.Setup(x => x.UpdateFavorite(request.Id, It.IsAny<Favorites>())).Returns(Task.CompletedTask);

        // Act
        var result = await _manageFavoritesService.DeleteFavorite(request);

        // Assert
        Assert.True(result.IsSuccess);
        _getFavoriteQueryMock.Verify(x => x.GetFavoriteById(request.Id), Times.Once);
        _updateFavoriteCommandMock.Verify(x => x.UpdateFavorite(request.Id, It.Is<Favorites>(f => f.Deleted == true)), Times.Once);
    }

    [Fact]
    public async Task DeleteFavorite_ShouldReturnError_WhenFavoriteNotFound()
    {
        // Arrange
        var request = new DeleteFavoriteRequest { Id = 1 };
        _getFavoriteQueryMock.Setup(x => x.GetFavoriteById(request.Id)).ReturnsAsync((Favorites?)null);

        // Act
        var result = await _manageFavoritesService.DeleteFavorite(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
        _updateFavoriteCommandMock.Verify(x => x.UpdateFavorite(It.IsAny<int>(), It.IsAny<Favorites>()), Times.Never);
    }

    [Fact]
    public async Task DeleteFavorite_ShouldReturnError_WhenExceptionThrown()
    {
        // Arrange
        var request = new DeleteFavoriteRequest { Id = 1 };
        _getFavoriteQueryMock.Setup(x => x.GetFavoriteById(request.Id)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _manageFavoritesService.DeleteFavorite(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("error", result.ErrorMessage.ToLower());
    }
}
