using AutoMapper;
using Moq;
using ReelRating.Application.Services.CineServices;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CineSchema.Request;
using ReelRating.Core.Schema.CineSchema.Response;
using ReelRating.Data.Query.CineQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Tests.Services;

public class SearchCineServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IListCineByYear> _listCineByYearMock;
    private readonly Mock<IGetCineByNameQuery> _getCineByNameQueryMock;
    private readonly Mock<IListCineByCategoriesAndYearQuery> _listCineByCategoriesAndYearMock;
    private readonly SearchCineService _searchCineService;

    public SearchCineServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _listCineByYearMock = new Mock<IListCineByYear>();
        _getCineByNameQueryMock = new Mock<IGetCineByNameQuery>();
        _listCineByCategoriesAndYearMock = new Mock<IListCineByCategoriesAndYearQuery>();

        _searchCineService = new SearchCineService(
            _mapperMock.Object,
            _listCineByYearMock.Object,
            _getCineByNameQueryMock.Object,
            _listCineByCategoriesAndYearMock.Object
        );
    }

    [Fact]
    public async Task SearchCineDefault_ShouldReturnSuccess_WhenCinesFound()
    {
        // Arrange
        var request = new ListCineDefaultRequest { PageNumber = 1, PageSize = 10 };
        var cines = new List<Cine>
        {
            new() { Id = 1, Name = "Movie 1", Year = 2024 },
            new() { Id = 2, Name = "Movie 2", Year = 2024 }
        };
        var cineResponses = new List<CineResponse>
        {
            new() { Id = 1, Name = "Movie 1" },
            new() { Id = 2, Name = "Movie 2" }
        };

        _listCineByYearMock
            .Setup(x => x.ListCineByYearAsync(It.IsAny<int>(), request.PageNumber, request.PageSize))
            .ReturnsAsync(cines);

        _mapperMock
            .Setup(x => x.Map<List<CineResponse>>(cines))
            .Returns(cineResponses);

        // Act
        var result = await _searchCineService.SearchCineDefault(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);
        _listCineByYearMock.Verify(x => x.ListCineByYearAsync(It.IsAny<int>(), request.PageNumber, request.PageSize), Times.Once);
    }

    [Fact]
    public async Task SearchCineDefault_ShouldReturnError_WhenExceptionThrown()
    {
        // Arrange
        var request = new ListCineDefaultRequest { PageNumber = 1, PageSize = 10 };
        _listCineByYearMock
            .Setup(x => x.ListCineByYearAsync(It.IsAny<int>(), request.PageNumber, request.PageSize))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _searchCineService.SearchCineDefault(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("error", result.ErrorMessage.ToLower());
    }

    [Fact]
    public async Task SearchCineByName_ShouldReturnSuccess_WhenCineFound()
    {
        // Arrange
        var request = new CineRequest { Name = "Test Movie" };
        var cine = new Cine { Id = 1, Name = "Test Movie", Year = 2024 };
        var cineResponse = new CineResponse { Id = 1, Name = "Test Movie" };

        _getCineByNameQueryMock
            .Setup(x => x.GetCineByNameAsync(request.Name))
            .ReturnsAsync(cine);

        _mapperMock
            .Setup(x => x.Map<CineResponse>(cine))
            .Returns(cineResponse);

        // Act
        var result = await _searchCineService.SearchCineByName(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal("Test Movie", result.Data.Name);
        _getCineByNameQueryMock.Verify(x => x.GetCineByNameAsync(request.Name), Times.Once);
    }

    [Fact]
    public async Task SearchCineByName_ShouldReturnError_WhenCineNotFound()
    {
        // Arrange
        var request = new CineRequest { Name = "Nonexistent Movie" };
        _getCineByNameQueryMock
            .Setup(x => x.GetCineByNameAsync(request.Name))
            .ThrowsAsync(new Exception($"Cine with name '{request.Name}' not found."));

        // Act
        var result = await _searchCineService.SearchCineByName(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
    }

    [Fact]
    public async Task SearchCineByFilters_ShouldReturnSuccess_WhenCinesFound()
    {
        // Arrange
        var request = new ListCineByFiltersRequest { CategoriesId = 1, Year = 2024, PageNumber = 1, PageSize = 10 };
        var cines = new List<Cine>
        {
            new() { Id = 1, Name = "Movie 1", Year = 2024 }
        };
        var cineResponses = new List<CineResponse>
        {
            new() { Id = 1, Name = "Movie 1" }
        };

        _listCineByCategoriesAndYearMock
            .Setup(x => x.ListCineByCategoriesAndYearAsync(request.CategoriesId, request.Year, request.PageNumber, request.PageSize))
            .ReturnsAsync((cines, 1));

        _mapperMock
            .Setup(x => x.Map<List<CineResponse>>(cines))
            .Returns(cineResponses);

        // Act
        var result = await _searchCineService.SearchCineByFilters(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Single(result.Data);
        _listCineByCategoriesAndYearMock.Verify(x => x.ListCineByCategoriesAndYearAsync(request.CategoriesId, request.Year, request.PageNumber, request.PageSize), Times.Once);
    }

    [Fact]
    public async Task SearchCineByFilters_ShouldReturnError_WhenExceptionThrown()
    {
        // Arrange
        var request = new ListCineByFiltersRequest { CategoriesId = 1, Year = 2024, PageNumber = 1, PageSize = 10 };
        _listCineByCategoriesAndYearMock
            .Setup(x => x.ListCineByCategoriesAndYearAsync(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _searchCineService.SearchCineByFilters(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("error", result.ErrorMessage.ToLower());
    }
}
