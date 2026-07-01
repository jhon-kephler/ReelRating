using MediatR;
using Moq;
using ReelRating.Application.Handler.CineHandler;
using ReelRating.Application.Services.CineServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CineSchema.Request;
using ReelRating.Core.Schema.CineSchema.Response;
using ReelRating.Core.Schema.HomeSchema.Request;

namespace ReelRating.Tests.Handlers;

public class SearchCineHandlerTests
{
    private readonly Mock<ISearchCineServices> _searchCineServicesMock;
    private readonly SearchCineHandler _searchCineHandler;

    public SearchCineHandlerTests()
    {
        _searchCineServicesMock = new Mock<ISearchCineServices>();
        _searchCineHandler = new SearchCineHandler(_searchCineServicesMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginationResult_WhenRequestIsValid()
    {
        // Arrange
        var request = new ListCineDefaultRequest { PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CineResponse>();
        expectedResult.SetSuccess(new List<CineResponse> { new() { Id = 1, Name = "Movie 1" } }, 1, 10, 1);

        _searchCineServicesMock
            .Setup(x => x.SearchCineDefault(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchCineHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _searchCineServicesMock.Verify(x => x.SearchCineDefault(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new ListCineDefaultRequest { PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CineResponse>();

        _searchCineServicesMock
            .Setup(x => x.SearchCineDefault(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _searchCineHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchCineServicesMock.Verify(x => x.SearchCineDefault(request), Times.Once);
    }
}
