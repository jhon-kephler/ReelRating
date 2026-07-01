using MediatR;
using Moq;
using ReelRating.Application.Handler.HomeHandler;
using ReelRating.Application.Services.HomeServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.HomeSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Response;

namespace ReelRating.Tests.Handlers.SearchAllCategoriesTests;

public class SearchAllCategoriesHandlerTests
{
    private readonly Mock<ISearchFiltersServices> _searchFiltersServicesMock;
    private readonly SearchAllCategoriesHandler _searchAllCategoriesHandler;

    public SearchAllCategoriesHandlerTests()
    {
        _searchFiltersServicesMock = new Mock<ISearchFiltersServices>();
        _searchAllCategoriesHandler = new SearchAllCategoriesHandler(_searchFiltersServicesMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginationResult_WhenRequestIsValid()
    {
        // Arrange
        var request = new CategoriesRequest { PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CategoriesResponse>();
        expectedResult.SetSuccess(new List<CategoriesResponse> { new() { Id = 1, Name = "Action" } }, 1, 10, 1);

        _searchFiltersServicesMock
            .Setup(x => x.GetCategories(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchAllCategoriesHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _searchFiltersServicesMock.Verify(x => x.GetCategories(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new CategoriesRequest { PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CategoriesResponse>();

        _searchFiltersServicesMock
            .Setup(x => x.GetCategories(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _searchAllCategoriesHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchFiltersServicesMock.Verify(x => x.GetCategories(request), Times.Once);
    }
}
