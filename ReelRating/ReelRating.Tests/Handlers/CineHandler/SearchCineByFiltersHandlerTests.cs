using MediatR;
using Moq;
using ReelRating.Application.Handler.CineHandler;
using ReelRating.Application.Services.CineServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CineSchema.Request;
using ReelRating.Core.Schema.CineSchema.Response;

namespace ReelRating.Tests.Handlers.SearchCineByFiltersTests;

public class SearchCineByFiltersHandlerTests
{
    private readonly Mock<ISearchCineServices> _searchCineServicesMock;
    private readonly SearchCineByFiltersHandler _searchCineByFiltersHandler;

    public SearchCineByFiltersHandlerTests()
    {
        _searchCineServicesMock = new Mock<ISearchCineServices>();
        _searchCineByFiltersHandler = new SearchCineByFiltersHandler(_searchCineServicesMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginationResult_WhenRequestIsValid()
    {
        // Arrange
        var request = new ListCineByFiltersRequest { CategoriesId = 1, Year = 2024, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CineResponse>();
        expectedResult.SetSuccess(new List<CineResponse> { new() { Id = 1, Name = "Movie 1" } }, 1, 10, 1);

        _searchCineServicesMock
            .Setup(x => x.SearchCineByFilters(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchCineByFiltersHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _searchCineServicesMock.Verify(x => x.SearchCineByFilters(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new ListCineByFiltersRequest { CategoriesId = 1, Year = 2024, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CineResponse>();

        _searchCineServicesMock
            .Setup(x => x.SearchCineByFilters(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _searchCineByFiltersHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchCineServicesMock.Verify(x => x.SearchCineByFilters(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenServiceReturnsError()
    {
        // Arrange
        var request = new ListCineByFiltersRequest { CategoriesId = 1, Year = 2024, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CineResponse>();
        expectedResult.ValidateResult("No cine found");

        _searchCineServicesMock
            .Setup(x => x.SearchCineByFilters(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchCineByFiltersHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _searchCineServicesMock.Verify(x => x.SearchCineByFilters(request), Times.Once);
    }
}
