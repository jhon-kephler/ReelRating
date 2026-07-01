using AutoMapper;
using Moq;
using ReelRating.Application.Services.FavoritesServices;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;
using ReelRating.Data.Query.FavoritesQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Tests.Services.SearchFavoritesTests;

public class SearchFavoritesServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IListFavoritesQuery> _listFavoritesQueryMock;
    private readonly Mock<IGetFavoriteQuery> _getFavoriteQueryMock;
    private readonly SearchFavoritesService _searchFavoritesService;

    public SearchFavoritesServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _listFavoritesQueryMock = new Mock<IListFavoritesQuery>();
        _getFavoriteQueryMock = new Mock<IGetFavoriteQuery>();

        _searchFavoritesService = new SearchFavoritesService(
            _mapperMock.Object,
            _listFavoritesQueryMock.Object,
            _getFavoriteQueryMock.Object
        );
    }

    [Fact]
    public async Task SearchFavoriteById_ShouldReturnSuccess_WhenFavoriteFound()
    {
        // Arrange
        var request = new SearchFavoriteByCustomerIdRequest { Id = 1, CustomerId = 1 };
        var favorite = new Favorites { Id = 1, CustomerId = 1, CineId = 1 };
        var favoriteResponse = new FavoritesResponse { Id = 1, CustomerId = 1, CineId = 1 };

        _getFavoriteQueryMock
            .Setup(x => x.GetFavoriteByIdAndCustomerId(request.Id, request.CustomerId))
            .ReturnsAsync(favorite);

        _mapperMock
            .Setup(x => x.Map<FavoritesResponse>(favorite))
            .Returns(favoriteResponse);

        // Act
        var result = await _searchFavoritesService.SearchFavoriteById(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
        _getFavoriteQueryMock.Verify(x => x.GetFavoriteByIdAndCustomerId(request.Id, request.CustomerId), Times.Once);
    }

    [Fact]
    public async Task SearchFavoriteById_ShouldReturnError_WhenFavoriteNotFound()
    {
        // Arrange
        var request = new SearchFavoriteByCustomerIdRequest { Id = 1, CustomerId = 1 };
        _getFavoriteQueryMock
            .Setup(x => x.GetFavoriteByIdAndCustomerId(request.Id, request.CustomerId))
            .ReturnsAsync((Favorites?)null);

        // Act
        var result = await _searchFavoritesService.SearchFavoriteById(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
    }

    [Fact]
    public async Task SearchFavoriteByIdAndCineId_ShouldReturnSuccess_WhenFavoriteFound()
    {
        // Arrange
        var request = new SearchFavoriteByCustomerIdAndCineIdRequest { Id = 1, CustomerId = 1, CineId = 1 };
        var favorite = new Favorites { Id = 1, CustomerId = 1, CineId = 1 };
        var favoriteResponse = new FavoritesResponse { Id = 1, CustomerId = 1, CineId = 1 };

        _getFavoriteQueryMock
            .Setup(x => x.GetFavoriteByIdAndCustomerIdAndCineId(request.Id, request.CustomerId, request.CineId))
            .ReturnsAsync(favorite);

        _mapperMock
            .Setup(x => x.Map<FavoritesResponse>(favorite))
            .Returns(favoriteResponse);

        // Act
        var result = await _searchFavoritesService.SearchFavoriteByIdAndCineId(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        _getFavoriteQueryMock.Verify(x => x.GetFavoriteByIdAndCustomerIdAndCineId(request.Id, request.CustomerId, request.CineId), Times.Once);
    }

    [Fact]
    public async Task ListFavorites_ShouldReturnSuccess_WhenFavoritesExist()
    {
        // Arrange
        var request = new ListFavoritesRequest { PageNumber = 1, PageSize = 10 };
        var favorites = new List<Favorites>
        {
            new() { Id = 1, CustomerId = 1, CineId = 1 },
            new() { Id = 2, CustomerId = 1, CineId = 2 }
        };
        var favoriteResponses = new List<FavoritesResponse>
        {
            new() { Id = 1, CustomerId = 1, CineId = 1 },
            new() { Id = 2, CustomerId = 1, CineId = 2 }
        };

        _listFavoritesQueryMock
            .Setup(x => x.ListFavorites(request.PageNumber, request.PageSize))
            .ReturnsAsync(favorites);

        _mapperMock
            .Setup(x => x.Map<List<FavoritesResponse>>(favorites))
            .Returns(favoriteResponses);

        // Act
        var result = await _searchFavoritesService.ListFavorites(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);
        _listFavoritesQueryMock.Verify(x => x.ListFavorites(request.PageNumber, request.PageSize), Times.Once);
    }

    [Fact]
    public async Task ListFavoritesById_ShouldReturnSuccess_WhenFavoritesExist()
    {
        // Arrange
        var request = new ListFavoritesByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        var favorites = new List<Favorites>
        {
            new() { Id = 1, CustomerId = 1, CineId = 1 }
        };
        var favoriteResponses = new List<FavoritesResponse>
        {
            new() { Id = 1, CustomerId = 1, CineId = 1 }
        };

        _listFavoritesQueryMock
            .Setup(x => x.ListFavoritesById(request.Id, request.PageNumber, request.PageSize))
            .ReturnsAsync(favorites);

        _mapperMock
            .Setup(x => x.Map<List<FavoritesResponse>>(favorites))
            .Returns(favoriteResponses);

        // Act
        var result = await _searchFavoritesService.ListFavoritesById(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Single(result.Data);
        _listFavoritesQueryMock.Verify(x => x.ListFavoritesById(request.Id, request.PageNumber, request.PageSize), Times.Once);
    }

    [Fact]
    public async Task ListFavoritesById_ShouldReturnError_WhenFavoritesNotFound()
    {
        // Arrange
        var request = new ListFavoritesByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        _listFavoritesQueryMock
            .Setup(x => x.ListFavoritesById(request.Id, request.PageNumber, request.PageSize))
            .ReturnsAsync((List<Favorites>?)null);

        // Act
        var result = await _searchFavoritesService.ListFavoritesById(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
    }
}
