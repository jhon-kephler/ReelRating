using MediatR;
using Moq;
using ReelRating.Application.Handler.FiltersHandler;
using ReelRating.Application.Services.HomeServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FiltersSchema.Request;

namespace ReelRating.Tests.Handlers.SearchAllYearTests;

public class SearchAllYearHandlerTests
{
    private readonly Mock<ISearchFiltersServices> _searchFiltersServicesMock;
    private readonly SearchAllYearHandler _searchAllYearHandler;

    public SearchAllYearHandlerTests()
    {
        _searchFiltersServicesMock = new Mock<ISearchFiltersServices>();
        _searchAllYearHandler = new SearchAllYearHandler(_searchFiltersServicesMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginationResult_WhenRequestIsValid()
    {
        // Arrange
        var request = new YearRequest { PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<int?>();
        expectedResult.SetSuccess(new List<int?> { 2020, 2021, 2022 }, 1, 10, 3);

        _searchFiltersServicesMock
            .Setup(x => x.GetYear(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchAllYearHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _searchFiltersServicesMock.Verify(x => x.GetYear(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new YearRequest { PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<int?>();

        _searchFiltersServicesMock
            .Setup(x => x.GetYear(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _searchAllYearHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchFiltersServicesMock.Verify(x => x.GetYear(request), Times.Once);
    }
}
