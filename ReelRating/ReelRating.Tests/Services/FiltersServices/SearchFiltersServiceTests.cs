using AutoMapper;
using Moq;
using ReelRating.Application.Services.HomeServices;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FiltersSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Response;
using ReelRating.Data.Query.CineQuery;
using ReelRating.Data.Query.FiltersQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Tests.Services.SearchFiltersTests;

public class SearchFiltersServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IListCategories> _listCategoriesMock;
    private readonly Mock<IListYearQuery> _listYearQueryMock;
    private readonly SearchFiltersService _searchFiltersService;

    public SearchFiltersServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _listCategoriesMock = new Mock<IListCategories>();
        _listYearQueryMock = new Mock<IListYearQuery>();

        _searchFiltersService = new SearchFiltersService(
            _mapperMock.Object,
            _listCategoriesMock.Object,
            _listYearQueryMock.Object
        );
    }

    [Fact]
    public async Task GetCategories_ShouldReturnSuccess_WhenCategoriesExist()
    {
        // Arrange
        var request = new ReelRating.Core.Schema.HomeSchema.Request.CategoriesRequest { PageNumber = 1, PageSize = 10 };
        var categories = new List<Categories>
        {
            new() { Id = 1, Name = "Action" },
            new() { Id = 2, Name = "Comedy" }
        };
        var categoryResponses = new List<CategoriesResponse>
        {
            new() { Id = 1, Name = "Action" },
            new() { Id = 2, Name = "Comedy" }
        };

        _listCategoriesMock
            .Setup(x => x.GetAllAsync(request.PageNumber, request.PageSize))
            .ReturnsAsync(categories);

        _mapperMock
            .Setup(x => x.Map<List<CategoriesResponse>>(categories))
            .Returns(categoryResponses);

        // Act
        var result = await _searchFiltersService.GetCategories(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);
        _listCategoriesMock.Verify(x => x.GetAllAsync(request.PageNumber, request.PageSize), Times.Once);
    }

    [Fact]
    public async Task GetCategories_ShouldReturnError_WhenExceptionThrown()
    {
        // Arrange
        var request = new ReelRating.Core.Schema.HomeSchema.Request.CategoriesRequest { PageNumber = 1, PageSize = 10 };
        _listCategoriesMock
            .Setup(x => x.GetAllAsync(request.PageNumber, request.PageSize))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _searchFiltersService.GetCategories(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("error", result.ErrorMessage.ToLower());
    }

    [Fact]
    public async Task GetYear_ShouldReturnSuccess_WhenYearsExist()
    {
        // Arrange
        var request = new YearRequest { PageNumber = 1, PageSize = 10 };
        var years = new List<int?> { 2020, 2021, 2022, 2023, 2024 };

        _listYearQueryMock
            .Setup(x => x.GetAllYearAsync(request.PageNumber, request.PageSize))
            .ReturnsAsync(years);

        // Act
        var result = await _searchFiltersService.GetYear(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(5, result.Data.Count);
        _listYearQueryMock.Verify(x => x.GetAllYearAsync(request.PageNumber, request.PageSize), Times.Once);
    }

    [Fact]
    public async Task GetYear_ShouldReturnError_WhenExceptionThrown()
    {
        // Arrange
        var request = new YearRequest { PageNumber = 1, PageSize = 10 };
        _listYearQueryMock
            .Setup(x => x.GetAllYearAsync(request.PageNumber, request.PageSize))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _searchFiltersService.GetYear(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("error", result.ErrorMessage.ToLower());
    }

    [Fact]
    public async Task GetCategories_ShouldReturnEmptyList_WhenNoCategoriesExist()
    {
        // Arrange
        var request = new ReelRating.Core.Schema.HomeSchema.Request.CategoriesRequest { PageNumber = 1, PageSize = 10 };
        var categories = new List<Categories>();
        var categoryResponses = new List<CategoriesResponse>();

        _listCategoriesMock
            .Setup(x => x.GetAllAsync(request.PageNumber, request.PageSize))
            .ReturnsAsync(categories);

        _mapperMock
            .Setup(x => x.Map<List<CategoriesResponse>>(categories))
            .Returns(categoryResponses);

        // Act
        var result = await _searchFiltersService.GetCategories(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);
    }

    [Fact]
    public async Task GetYear_ShouldReturnEmptyList_WhenNoYearsExist()
    {
        // Arrange
        var request = new YearRequest { PageNumber = 1, PageSize = 10 };
        var years = new List<int?>();

        _listYearQueryMock
            .Setup(x => x.GetAllYearAsync(request.PageNumber, request.PageSize))
            .ReturnsAsync(years);

        // Act
        var result = await _searchFiltersService.GetYear(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);
    }
}
